using System.Collections.Generic;
using System;

namespace SoapWebservices
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

        public bool HasBody()
        {
            return false;
        }

        public bool CanSendBody()
        {
            return false;
        }

        public byte[] ToBytes()
        {
            throw new Exception("data on a get is not supported");
        }

        public IDictionary<string, string> Headers
        {
            get { return new Dictionary<string, string>(); }
        }

        public HttpBody GetBody()
        {
            return HttpBody.EMPTY;
        }
    }
}
