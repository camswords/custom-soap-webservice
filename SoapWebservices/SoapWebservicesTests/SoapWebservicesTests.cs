using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Net;
using SoapWebservices;

namespace SoapWebservicesTests
{
    [TestFixture]
    public class SoapWebservicesTests
    {

        [Test]
        public void should_send_request_for_a_get_method()
        {
            var server = new WebServer((request, responseStream) =>
            {
                Assert.That(request, Text.Contains("GET / HTTP/1.1"));
                
                responseStream.WriteLine("HTTP/1.1 200 OK");
                responseStream.WriteLine("");
            });

            server.Start();

            new HttpGateway().Get(new HttpGet(string.Format("http://localhost:{0}/", server.PortNumber)));
        }

        [Test]
        public void should_parse_response_for_get_request()
        {
            var server = new WebServer((request, responseStream) =>
            {
                responseStream.WriteLine("HTTP/1.1 200 OK");
                responseStream.WriteLine("");
                responseStream.Write("yeah");
            });

            server.Start();

            var response = new HttpGateway().Get(new HttpGet(string.Format("http://localhost:{0}", server.PortNumber)));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.StatusDescription, Is.EqualTo("OK"));
            Assert.That(response.Content, Is.EqualTo("yeah"));
            Assert.That(response.Failed, Is.False);
        }

        [Test]
        public void should_return_failed_response_when_server_returns_internal_server_error()
        {
            var server = new WebServer((request, responseStream) =>
            {
                responseStream.WriteLine("HTTP/1.1 500 Internal Server Error");
                responseStream.WriteLine("");
                responseStream.Write("i just dont know what to do with myself...");
            });

            server.Start();

            var response = new HttpGateway().Get(new HttpGet(string.Format("http://localhost:{0}", server.PortNumber)));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
            Assert.That(response.StatusDescription, Is.EqualTo("Internal Server Error"));
            Assert.That(response.Content, Is.EqualTo("i just dont know what to do with myself..."));
            Assert.That(response.Failed, Is.True);
        }
    }
}