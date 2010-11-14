using System.Text;

namespace SoapWebservicesTests.Http
{
    public class RequestBody
    {
        private readonly string body;

        public RequestBody(char[] body)
        {
            this.body = new string(body);
        }

        public string GetContent()
        {
            return body;
        }
    }
}
