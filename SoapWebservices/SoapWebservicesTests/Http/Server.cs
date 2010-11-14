using System.Net.Sockets;
using System.IO;
using System.Net;
using System;
using System.Threading;
using SoapWebservices;
using SoapWebservicesTests.Http.RequestHandler;
using SoapWebservicesTests.Http.Request;

namespace SoapWebservicesTests.Http
{
    public class WebServer
    {
        private TcpListener serverSocket;
        private IRequestHandler requestHandler;
        private Thread listeningThread;
        private int portNumber;

        public WebServer(Action<HttpRequest, TextWriter> requestHandler) : 
           this(new DelegateRequestHandler(requestHandler))
        {
        }

        public WebServer(IRequestHandler requestHandler)
        {
            this.requestHandler = new ExceptionHandlingRequestHandler(requestHandler);
            this.portNumber = new PortNumbers().LocateUnused();
        }

        public void Start()
        {
            var localhost = Dns.GetHostEntry("localhost").AddressList[0];
            serverSocket = new TcpListener(localhost, portNumber);
            serverSocket.Start();

            listeningThread = new Thread(Listen);
            listeningThread.Start();
        }

        private void Listen()
        {
            var tcpClient = serverSocket.AcceptTcpClient();
            var clientConnection = tcpClient.GetStream();

            var response = new StreamWriter(clientConnection);
            var request = new HttpRequestReader().Read(clientConnection);

            try
            {
                requestHandler.Handle(request, response);
                response.Flush();
            }
            finally
            {
                new SuppressExceptions().On(() => response.Dispose());
                new SuppressExceptions().On(() => clientConnection.Dispose());
                new SuppressExceptions().On(() => tcpClient.Close());
                new SuppressExceptions().On(() => serverSocket.Stop());
            }

            listeningThread.Join(10000);
        }

        public int PortNumber
        {
            get { return portNumber; }
        }
    }
}
