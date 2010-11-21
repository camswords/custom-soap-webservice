using Antlr3.ST;
using System;
using System.IO;
using System.Reflection;
using SoapWebservices.Text;
using System.Collections.Generic;

namespace SoapWebservices
{
    public class RandomGoogleSearchMessageBuilder
    {
        public string Build()
        {
            var template = new Template("SoapWebservices.Templates.GoogleSearch.search-request.st");
            var parameters = new Dictionary<string, object>();
            return template.Render(parameters);
        }
    }
}
