using System.IO;
using System.Net.Sockets;

namespace SoapWebservicesTests
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

            var request = "";
            string line = null;

            do
            {
                line = reader.ReadLine();
                request += line + "\n";
            } while (line.Length != 0);

            return request;
        }
    }
}
