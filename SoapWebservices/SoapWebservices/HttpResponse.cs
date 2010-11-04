using System.Net;

namespace SoapWebservices
{
    public class HttpResponse
    {
        private readonly string content;
        private readonly HttpStatusCode statusCode;
        private readonly string statusDescription;

        public HttpResponse(HttpStatusCode statusCode, string statusDescription, string content)
        {
            this.content = content;
            this.statusCode = statusCode;
            this.statusDescription = statusDescription;
        }

        public string Content
        {
            get { return content; }
        }

        public HttpStatusCode StatusCode
        {
            get { return statusCode; }
        }

        public string StatusDescription
        {
            get { return statusDescription; }
        }
    }
}
