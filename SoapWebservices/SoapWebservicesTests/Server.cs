using System.Net.Sockets;
using System.IO;
using System.Net;
using System;
using System.Threading;

namespace SoapWebservicesTests
{
    public class WebServer
    {
        private TcpListener serverSocket;
        private IRequestHandler requestHandler;
        private Thread listeningThread;

        public WebServer(Action<string, StreamWriter> responseWriter)
        {
            this.requestHandler = new DelegateRequestHandler(responseWriter);

            var localhost = Dns.Resolve("localhost").AddressList[0];
            serverSocket = new TcpListener(localhost, 5021);
            serverSocket.Start();

            listeningThread = new Thread(Listen);
            listeningThread.Start();
        }

        public void Listen()
        {
            var tcpClient = serverSocket.AcceptTcpClient();
            var clientConnection = tcpClient.GetStream();

            var response = new StreamWriter(clientConnection);
            var request = new HttpRequest(clientConnection).ReadHeader();

            requestHandler.Handle(request, response);

            response.Flush();
            clientConnection.Dispose();

            listeningThread.Join(10000);
        }
    }
}
