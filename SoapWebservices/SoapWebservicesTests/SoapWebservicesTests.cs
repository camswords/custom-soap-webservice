using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Net;
using System.Text;
using SoapWebservices;
using SoapWebservicesTests.Http;
using System.IO;
using System;
using SoapWebservicesTests.Http.RequestHandler;

namespace SoapWebservicesTests
{
    [TestFixture]
    public class SoapWebservicesTests
    {
        [Test]
        public void should_send_request_for_a_get_method()
        {
            var recordingRequestHandler = new RecordingRequestHandler();
            var server = new WebServer(recordingRequestHandler);
            server.Start();

            new HttpGateway().Execute(new HttpGet(string.Format("http://localhost:{0}/", server.PortNumber)));

            var recordedRequest = recordingRequestHandler.LastRecordedRequest;
            Assert.That(recordedRequest.StatusLine, Text.Contains("GET / HTTP/1.1"));
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

            var response = new HttpGateway().Execute(new HttpGet(string.Format("http://localhost:{0}", server.PortNumber)));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.StatusDescription, Is.EqualTo("OK"));
            Assert.That(response.Content, Is.EqualTo("yeah"));
            Assert.That(response.Failed, Is.False);
        }

        [Test]
        public void should_return_failed_response_when_server_returns_internal_server_error_on_get_request()
        {
            var server = new WebServer((request, responseStream) =>
            {
                responseStream.WriteLine("HTTP/1.1 500 Internal Server Error");
                responseStream.WriteLine("");
                responseStream.Write("i just dont know what to do with myself...");
            });

            server.Start();

            var response = new HttpGateway().Execute(new HttpGet(string.Format("http://localhost:{0}", server.PortNumber)));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
            Assert.That(response.StatusDescription, Is.EqualTo("Internal Server Error"));
            Assert.That(response.Content, Is.EqualTo("i just dont know what to do with myself..."));
            Assert.That(response.Failed, Is.True);
        }

        [Test]
        public void should_post_empty_request_to_server()
        {
            var requestHandler = new RecordingRequestHandler();
            var server = new WebServer(requestHandler);
            server.Start();

            var uri = string.Format("http://localhost:{0}", server.PortNumber);
            new HttpGateway().Execute(new HttpPost(uri, string.Empty, "text/xml", "utf-8"));

            var recordedRequest = requestHandler.LastRecordedRequest;
            Assert.That(recordedRequest.StatusLine, Is.EqualTo("POST / HTTP/1.1"));
            Assert.That(recordedRequest.Header.GetContentLength(), Is.EqualTo(0));
        }

        [Test]
        public void should_post_request_with_data_to_server()
        {
            var requestHandler = new RecordingRequestHandler();
            var server = new WebServer(requestHandler);
            server.Start();

            var uri = string.Format("http://localhost:{0}", server.PortNumber);
            new HttpGateway().Execute(new HttpPost(uri, "test.data", "text/xml", "utf-8"));

            var recordedRequest = requestHandler.LastRecordedRequest;
            Assert.That(recordedRequest.StatusLine, Text.Contains("POST / HTTP/1.1"));
            Assert.That(recordedRequest.Header.GetContentType(), Is.EqualTo("text/xml;charset=utf-8"));
            Assert.That(recordedRequest.Header.GetContentLength(), Is.EqualTo(9));
            Assert.That(recordedRequest.Body.GetContent(), Is.EqualTo("test.data"));
        }

        [Test]
        public void should_return_failed_response_when_server_returns_internal_server_error_on_post_request()
        {
            var server = new WebServer((request, responseStream) =>
            {
                responseStream.WriteLine("HTTP/1.1 500 Internal Server Error");
                responseStream.WriteLine("");
                responseStream.Write("i just dont know what to do with myself...");
            });

            server.Start();

            var uri = "http://localhost:" + server.PortNumber;
            var response = new HttpGateway().Execute(new HttpPost(uri, "test.data", "text/xml", "utf-8"));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
            Assert.That(response.StatusDescription, Is.EqualTo("Internal Server Error"));
            Assert.That(response.Content, Is.EqualTo("i just dont know what to do with myself..."));
            Assert.That(response.Failed, Is.True);
        }
    }
}