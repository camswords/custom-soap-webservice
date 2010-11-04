using System.IO;
using System;

namespace SoapWebservicesTests
{
    public class DelegateRequestHandler : IRequestHandler
    {
        private Action<string, StreamWriter> responseWriter;

        public DelegateRequestHandler(Action<string, StreamWriter> responseWriter)
        {
            this.responseWriter = responseWriter;
        }

        public void Handle(string request, StreamWriter response)
        {
            responseWriter.Invoke(request, response);
        }
    }
}
