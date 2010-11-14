using System.Text;
using System;
using System.Collections.Generic;

namespace SoapWebservices
{
    public class HttpPost : HttpMethod
    {
        private readonly string uri;
        private readonly string data;
        private readonly string contentType;
        private readonly string charset;
        private readonly IDictionary<string, string> headers;

        public HttpPost(string uri, string data, string contentType, string charset)
        {
            this.uri = uri;
            this.data = data;
            this.contentType = contentType;
            this.charset = charset;
            this.headers = new Dictionary<string, string>();
        }

        public string Method
        {
            get { return "POST"; }
        }

        public string Uri
        {
            get { return uri; }
        }

        public bool CanSendBody()
        {
            return true;
        }

        public HttpBody GetBody()
        {
            var encodedData = Encoding.GetEncoding(charset).GetBytes(data);
            return new HttpBody(encodedData, contentType, charset);
        }

        public IDictionary<string, string> Headers
        {
            get { return headers; }
        }

        public void AddHeader(string name, string value)
        {
            headers.Add(name, value);
        }
    }
}
