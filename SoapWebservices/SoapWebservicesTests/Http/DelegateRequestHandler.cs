using System.IO;
using System;

namespace SoapWebservicesTests.Http
{
    public class DelegateRequestHandler : IRequestHandler
    {
        private Action<string, TextWriter> requestHandler;

        public DelegateRequestHandler(Action<string, TextWriter> requestHandler)
        {
            this.requestHandler = requestHandler;
        }

        public void Handle(string request, TextWriter response)
        {
            requestHandler.Invoke(request, response);
        }
    }
}
