﻿using System.IO;

namespace SoapWebservicesTests.Http
{
    public class RecordingRequestHandler : IRequestHandler
    {
        private string lastRecordedRequest = null;

        public void Handle(string requestContent, HttpRequest request, TextWriter response)
        {
            lastRecordedRequest = request.GetRawContent();

            response.WriteLine("HTTP/1.1 200 OK");
            response.WriteLine("");
        }

        public string LastRecordedRequest
        {
            get { return lastRecordedRequest; }
        }


    }
}