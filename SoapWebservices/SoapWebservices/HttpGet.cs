using System.Collections.Generic;

namespace SoapWebservices
{
    public class HttpGet
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
    }
}
