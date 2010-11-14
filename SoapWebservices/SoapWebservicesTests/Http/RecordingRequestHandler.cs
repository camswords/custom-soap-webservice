using System.IO;

namespace SoapWebservicesTests.Http
{
    public class RecordingRequestHandler : IRequestHandler
    {
        private HttpRequest lastRecordedRequest = null;

        public void Handle(HttpRequest request, TextWriter response)
        {
            lastRecordedRequest = request;

            response.WriteLine("HTTP/1.1 200 OK");
            response.WriteLine("");
        }

        public HttpRequest LastRecordedRequest
        {
            get { return lastRecordedRequest; }
        }
    }
}
