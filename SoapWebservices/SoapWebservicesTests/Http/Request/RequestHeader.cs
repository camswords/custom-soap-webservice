using System.Text.RegularExpressions;
using System;

namespace SoapWebservicesTests.Http.Request
{
    public class RequestHeader
    {
        private string header;

        public RequestHeader(string header)
        {
            this.header = header;
        }

        public string GetContentEncoding()
        {
            var contentType = GetString("Content-Type");
            var match = new Regex(".*;charset=(.*)").Match(contentType);
            if (!match.Success)
            {
                throw new ArgumentException(string.Format("failed to extract content type from content type header {0}", contentType));
            }
            return match.Groups[1].Value;
        }

        public string GetContentType()
        {
            return GetString("Content-Type");
        }

        public int GetContentLength()
        {
            return int.Parse(GetString("Content-Length"));
        }

        public bool HasBody()
        {
            return GetMethod() != "GET";
        }

        public string GetMethod()
        {
            var regex = new Regex("(.*) .* HTTP/1.1");
            var match = regex.Match(header);

            if (!match.Success)
            {
                throw new ArgumentException(string.Format("could not read method from header {0}", header));
            }
            return match.Groups[1].Value;
        }

        private string GetString(string key)
        {
            var regex = new Regex(key + ": (.*)");
            var match = regex.Match(header);

            if (!match.Success)
            {
                throw new ArgumentException(string.Format("failed to find header identified by key {0} in {1}", key, header));
            }
            return match.Groups[1].Value.Trim();
        }

        public string AsString()
        {
            return header;
        }
    }
}
