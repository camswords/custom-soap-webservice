using System.IO;
using System;
using SoapWebservicesTests.Http.Request;

namespace SoapWebservicesTests.Http.Request.Handler
{
    public class DelegateRequestHandler : IRequestHandler
    {
        private Action<HttpRequest, TextWriter> requestHandler;

        public DelegateRequestHandler(Action<HttpRequest, TextWriter> requestHandler)
        {
            this.requestHandler = requestHandler;
        }

        public void Handle(HttpRequest request, TextWriter response)
        {
            requestHandler.Invoke(request, response);
        }
    }
}
