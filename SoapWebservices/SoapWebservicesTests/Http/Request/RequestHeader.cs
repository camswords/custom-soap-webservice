using System.Text.RegularExpressions;
using System;

namespace SoapWebservicesTests.Http.Request
{
    public class RequestHeader
    {
        private static Regex CHARSET_CAPTURE = new Regex(".*;charset=(.*)");
        private static Regex METHOD_CAPTURE = new Regex("(.*) .* HTTP/1.1");

        private string header;

        public RequestHeader(string header)
        {
            this.header = header;
        }

        public string GetContentEncoding()
        {
            var contentType = GetHeader("Content-Type");
            var match = CHARSET_CAPTURE.Match(contentType);
            if (!match.Success)
            {
                throw new ArgumentException(string.Format("failed to extract content type from content type header {0}", contentType));
            }
            return match.Groups[1].Value;
        }

        public string GetContentType()
        {
            return GetHeader("Content-Type");
        }

        public int GetContentLength()
        {
            return int.Parse(GetHeader("Content-Length"));
        }

        public bool HasBody()
        {
            return GetMethod() != "GET" 
                   && HasHeader("Content-Length")
                   && HasHeader("Content-Type")
                   && GetContentLength() > 0;
        }

        public string GetMethod()
        {
            var match = METHOD_CAPTURE.Match(header);

            if (!match.Success)
            {
                throw new ArgumentException(string.Format("could not read method from header {0}", header));
            }
            return match.Groups[1].Value;
        }

        private bool HasHeader(string name)
        {
            var regex = new Regex(name + ": (.*)");
            var match = regex.Match(header);

            return match.Success;
        }

        private string GetHeader(string name)
        {
            var regex = new Regex(name + ": (.*)");
            var match = regex.Match(header);

            if (!match.Success)
            {
                throw new ArgumentException(string.Format("failed to find header identified by key {0} in {1}", name, header));
            }
            return match.Groups[1].Value.Trim();
        }

        public string AsString()
        {
            return header;
        }
    }
}
