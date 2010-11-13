using System.Text.RegularExpressions;
using System;

namespace SoapWebservicesTests.Http
{
    public class RequestHeader
    {
        private string header;

        public RequestHeader(string header)
        {
            this.header = header;
        }

        public string GetValue(string key)
        {
            var regex = new Regex(key + ": (.*)");
            var match = regex.Match(header);

            if (!match.Success)
            {
                throw new ArgumentException(string.Format("failed to find header identified by key {0} in {1}", key, header));
            }
            return match.Groups[1].Value.Trim();
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

        public string AsString()
        {
            return header;
        }
    }
}
