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

            if (header.HasBody())
            {
                var body = ReadBody(connection, header);
                return new HttpRequest(header, body).GetRawContent();
            }

            return new HttpRequest(header).GetRawContent();
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

        public RequestBody ReadBody(NetworkStream connection, RequestHeader header)
        {
            var encoding = Encoding.GetEncoding(header.GetContentEncoding());
            var requestReader = new StreamReader(connection, encoding);

            var body = new char[header.GetContentLength()];
            requestReader.Read(body, 0, header.GetContentLength());
            
            return new RequestBody(body);
        }
    }
}
