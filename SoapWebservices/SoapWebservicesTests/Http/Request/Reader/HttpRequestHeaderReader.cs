using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SoapWebservicesTests.Http.Request.Reader
{
    class HttpRequestHeaderReader
    {
        public RequestHeader Read(NetworkStream connection)
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
    }
}
