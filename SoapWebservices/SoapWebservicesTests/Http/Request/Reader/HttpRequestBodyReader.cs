using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SoapWebservicesTests.Http.Request.Reader
{
    public class HttpRequestBodyReader
    {
        public RequestBody Read(NetworkStream connection, RequestHeader header)
        {
            var encoding = Encoding.GetEncoding(header.GetContentEncoding());
            var requestReader = new StreamReader(connection, encoding);

            var body = new char[header.GetContentLength()];
            requestReader.Read(body, 0, header.GetContentLength());
            
            return new RequestBody(body);
        }
    }
}
