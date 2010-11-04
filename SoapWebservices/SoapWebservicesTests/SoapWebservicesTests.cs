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

namespace SoapWebservicesTests
{
    [TestFixture]
    public class SoapWebservicesTests
    {

        public class AnonymousRequestHandler : RequestHandler
        {
            public void Handle(string request, StreamWriter response)
            {
                response.WriteLine("HTTP/1.1 200 OK");
                response.WriteLine("");
                response.Write("yeah");
            }
        }

        [Test]
        public void should_return_response()
        {
            var svr = new Server(new AnonymousRequestHandler());
   
            Thread thread = new Thread(svr.Listen);
            thread.Start();

            var request = WebRequest.Create("http://localhost:5021");
            var response = (HttpWebResponse) request.GetResponse();

            var contents = "test.no.contents.returned";

            using(var s = new StreamReader(response.GetResponseStream()))
            {
                contents = s.ReadToEnd();
            }
            
            thread.Join(10000);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.StatusDescription, Is.EqualTo("OK"));
            Assert.That(contents, Is.EqualTo("yeah"));
        }
    }
}