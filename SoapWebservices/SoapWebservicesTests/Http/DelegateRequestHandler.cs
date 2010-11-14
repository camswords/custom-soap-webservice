using System.IO;
using System;

namespace SoapWebservicesTests.Http
{
    public class DelegateRequestHandler : IRequestHandler
    {
        private Action<string, HttpRequest, TextWriter> requestHandler;

        public DelegateRequestHandler(Action<string, HttpRequest, TextWriter> requestHandler)
        {
            this.requestHandler = requestHandler;
        }

        public void Handle(string requestContent, HttpRequest request, TextWriter response)
        {
            requestHandler.Invoke(requestContent, request, response);
        }
    }
}
