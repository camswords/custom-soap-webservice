using System.Net.Sockets;

namespace SoapWebservicesTests.Http.Request
{
    public class HttpRequestReader
    {
        public HttpRequest Read(NetworkStream connection)
        {
            var header = new HttpRequestHeaderReader().Read(connection);

            if (header.HasBody())
            {
                var body = new HttpRequestBodyReader().Read(connection, header);
                return new HttpRequest(header, body);
            }

            return new HttpRequest(header);
        }
    }
}
