using System.IO;
using System.Net.Sockets;
using System.Text;
using System;

namespace SoapWebservicesTests.Http
{
    public class HttpRequestReader
    {
        public string Read(NetworkStream connection)
        {
            var header = ReadHeader(connection);

            var request = new StringWriter();
            request.Write(header.AsString());

            if (header.HasBody())
            {
                request.Write(ReadBody(connection, header));
            }

            return request.ToString();
        }

        public RequestHeader ReadHeader(NetworkStream connection)
        {
            var requestReader = new StreamReader(connection);
            var request = new StringBuilder();
            string line = null;

            do
            {
                line = requestReader.ReadLine();
                request.AppendLine(line);
            } while (line.Length != 0);

            return new RequestHeader(request.ToString());
        }

        public string ReadBody(NetworkStream connection, RequestHeader header)
        {
            var contentLength = int.Parse(header.GetValue("Content-Length"));
            var encoding = header.GetValue("Content-Type");
            encoding = encoding.Substring(encoding.IndexOf(";"));
            encoding = encoding.Replace("charset=", "");
            encoding = encoding.Replace(";", "");
            
            var requestReader = new StreamReader(connection, Encoding.GetEncoding(encoding));

            var body = new char[contentLength];
            requestReader.Read(body, 0, contentLength);
            
            return new RequestBody(body).GetContent();
        }
    }
}
