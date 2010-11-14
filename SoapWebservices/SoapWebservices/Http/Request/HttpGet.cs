using System.Collections.Generic;
using System;

namespace SoapWebservices.Http.Request
{
    public class HttpGet : HttpMethod
    {
        private readonly string uri;

        public HttpGet(string uri)
        {
            this.uri = uri;
        }

        public string Method 
        {
            get { return "GET"; }
        }

        public string Uri 
        {
            get { return uri; }
        }

        public IDictionary<string, string> Headers
        {
            get { return new Dictionary<string, string>(); }
        }

        public bool CanSendBody()
        {
            return false;
        }

        public HttpBody GetBody()
        {
            return HttpBody.EMPTY;
        }
    }
}
