using System.Net;

namespace SoapWebservices
{
    public class HttpResponse
    {
        private readonly string content;

        public HttpResponse(HttpStatusCode statusCode, string statusDescription, string content)
        {
            this.content = content;
        }

        public string Content
        {
            get { return content; }
        }
    }
}
