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
        public void should_return_response()
        {
            var svr = new Server(reponseStream =>
            {
                reponseStream.WriteLine("HTTP/1.1 200 OK");
                reponseStream.WriteLine("");
                reponseStream.Write("yeah");
            });
   
            Thread thread = new Thread(svr.Listen);
            thread.Start();

            var response = new HttpGateway().Get(new HttpGet("http://localhost:5021"));

            thread.Join(10000);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.StatusDescription, Is.EqualTo("OK"));
            Assert.That(response.Content, Is.EqualTo("yeah"));
        }
    }
}