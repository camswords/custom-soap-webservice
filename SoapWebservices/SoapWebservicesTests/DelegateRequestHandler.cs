using System.IO;
using System;

namespace SoapWebservicesTests
{
    public class DelegateRequestHandler : IRequestHandler
    {
        private Action<StreamWriter> responseWriter;

        public DelegateRequestHandler(Action<StreamWriter> responseWriter)
        {
            this.responseWriter = responseWriter;
        }

        public void Handle(string request, StreamWriter response)
        {
            responseWriter.Invoke(response);
        }
    }
}
