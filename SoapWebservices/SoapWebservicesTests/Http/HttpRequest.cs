
namespace SoapWebservicesTests.Http
{
    public class HttpRequest
    {
        private readonly RequestHeader header;
        private readonly RequestBody body;

        public HttpRequest(RequestHeader header)
        {
            this.header = header;
            this.body = null;
        }

        public HttpRequest(RequestHeader header, RequestBody body)
        {
            this.header = header;
            this.body = body;
        }

        public string GetRawContent()
        {
            var headerValue = header.AsString();
            return body == null ? headerValue : headerValue + body.GetContent();
        }
    }
}
