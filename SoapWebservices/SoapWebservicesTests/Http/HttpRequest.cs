using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SoapWebservicesTests.Http
{
    public class HttpRequest
    {
        private NetworkStream connection;

        public HttpRequest(NetworkStream connection)
        {
            this.connection = connection;
        }

        public string ReadHeader()
        {
            var reader = new StreamReader(connection);

            var request = new StringBuilder();
            string line = null;

            do
            {
                line = reader.ReadLine();
                request.AppendLine(line);
            } while (line.Length != 0);

            return request.ToString();
        }
    }
}
