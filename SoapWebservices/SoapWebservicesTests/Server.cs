using System.Net.Sockets;
using System.IO;
using System.Net;
using System;

namespace SoapWebservicesTests
{
    public class Server
    {
        private TcpListener serverSocket;
        private IRequestHandler requestHandler;

        public Server(Action<StreamWriter> responseWriter)
        {
            this.requestHandler = new DelegateRequestHandler(responseWriter);

            var localhost = Dns.Resolve("localhost").AddressList[0];
            serverSocket = new TcpListener(localhost, 5021);
            serverSocket.Start();
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
        }
    }
}
