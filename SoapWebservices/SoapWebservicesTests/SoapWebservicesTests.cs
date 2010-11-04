using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using SoapWebservices;

namespace SoapWebservicesTests
{
    [TestFixture]
    public class SoapWebservicesTests
    {

        [Test]
        public void should_parse_response_for_get_request()
        {
            var server = new WebServer((request, responseStream) =>
            {
                responseStream.WriteLine("HTTP/1.1 200 OK");
                responseStream.WriteLine("");
                responseStream.Write("yeah");
            });

            var response = new HttpGateway().Get(new HttpGet("http://localhost:5021"));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.StatusDescription, Is.EqualTo("OK"));
            Assert.That(response.Content, Is.EqualTo("yeah"));
        }
    }
}