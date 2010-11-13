using System.IO;
using System;

namespace SoapWebservicesTests.Http
{
    public class DelegateRequestHandler : IRequestHandler
    {
        private Action<string, StreamWriter> requestHandler;

        public DelegateRequestHandler(Action<string, StreamWriter> requestHandler)
        {
            this.requestHandler = requestHandler;
        }

        public void Handle(string request, StreamWriter response)
        {
            requestHandler.Invoke(request, response);
        }
    }
}
