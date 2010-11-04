using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.Constraints;
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
                Printer.Print("begin accept request");
                var tcpClient = serverSocket.AcceptTcpClient();

                Printer.Print("creating sockets");
                var stream = tcpClient.GetStream();
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);

                var request = "";
                var response = "HTTP/1.1 200 OK\r\nContent-Length: 0\r\n\r\n";

                Printer.Print("reading");

                string line = null;
                do
                {
                    line = reader.ReadLine();
                    request += line + "\n";
                } while (line.Length != 0);

                Printer.Print("read request: " + request);
                

                Printer.Print("writing");

                writer.Write(response);
                writer.Flush();

                stream.Dispose();
        
                Printer.Print("done sockets!");
            }
        }

        [Test]
        public void should_return_response()
        {

            Printer.Print("initialising server");
            var svr = new Server();
   
            Thread thread = new Thread(svr.Begin);
            thread.Start();


            Printer.Print("server started, calling get");

            var request = WebRequest.Create("http://localhost:5021");
            Printer.Print("getting response");
            
            var response = request.GetResponse();

            var contents = "test.no.contents.returned";
            Printer.Print("reading response");

            using(var s = new StreamReader(response.GetResponseStream()))
            {
                Printer.Print("reading to end");
                contents = s.ReadToEnd();
            }
            
            Printer.Print("finished, lets join forces");

            thread.Join(10000);

            Printer.Print("read contents: " + contents);

            Printer.Print("ok, finished so joining threads");

            Assert.AreEqual("yeah", "yeah");
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