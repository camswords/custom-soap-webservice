using System.IO;
using System;

namespace SoapWebservicesTests.Http
{
    public class ExceptionHandlingRequestHandler : IRequestHandler
    {
        private readonly IRequestHandler requestHandler;

        public ExceptionHandlingRequestHandler(IRequestHandler requestHandler)
        {
            this.requestHandler = requestHandler;
        }

        public void Handle(string request, TextWriter response)
        {
            var writer = new StringWriter();

            try
            {
                requestHandler.Handle(request, writer);
                response.Write(writer.ToString());
            }
            catch (Exception e)
            {
                response.WriteLine("HTTP/1.1 500 failed to handle response");
                response.WriteLine("");
                response.WriteLine("failed handle response due to exception: " + e.Message);
                response.WriteLine("stacktrace: " + e.StackTrace);
                response.Write("content written: " + writer.ToString());
            }
        }
    }
}
