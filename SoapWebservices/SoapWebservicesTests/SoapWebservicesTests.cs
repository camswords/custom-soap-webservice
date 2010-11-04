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

        public class Server 
        {
            ManualResetEvent tcpConnectedClient;
            TcpListener serverSocket;

            public Server()
            {
                tcpConnectedClient = new ManualResetEvent(false);

                var localhost = Dns.Resolve("localhost").AddressList[0];
                serverSocket = new TcpListener(localhost, 5021);
                serverSocket.Start();
            }

           
            public void Begin()
            {
                var tcpClient = serverSocket.AcceptTcpClient();

                var stream = tcpClient.GetStream();
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);

                var request = "";

                string line = null;
                do
                {
                    line = reader.ReadLine();
                    request += line + "\n";
                } while (line.Length != 0);


                writer.WriteLine("HTTP/1.1 200 OK");
                writer.WriteLine("");
                writer.Write("yeah");
                writer.Flush();

                stream.Dispose();
            }
        }

        [Test]
        public void should_return_response()
        {
            var svr = new Server();
   
            Thread thread = new Thread(svr.Begin);
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

    public static class Printer
    {
        public static void Print(string message)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": " + message);
        }
    }
}