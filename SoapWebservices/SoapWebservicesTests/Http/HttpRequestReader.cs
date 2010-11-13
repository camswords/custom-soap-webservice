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
            var requestReader = new StreamReader(connection);
            var header = ReadHeader(requestReader);

            var request = new StringWriter();
            request.WriteLine(header.AsString());
            request.WriteLine();

            if (header.HasBody())
            {
                request.WriteLine(ReadBody(requestReader, header));
            }

            return request.ToString();
        }

        public RequestHeader ReadHeader(StreamReader requestReader)
        {
            var request = new StringBuilder();
            string line = null;

            do
            {
                line = requestReader.ReadLine();
                request.AppendLine(line);
            } while (line.Length != 0);

            return new RequestHeader(request.ToString());
        }

        public string ReadBody(StreamReader requestReader, RequestHeader header)
        {
            var contentLength = int.Parse(header.GetValue("Content-Length"));
            var body = new char[contentLength];
            requestReader.ReadBlock(body, 0, contentLength);

            return new string(body);
        }
    }
}
