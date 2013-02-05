using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace OhMyBoat.Network
{
    public class Server
    {
        private readonly TcpListener _listener;

        public Server()
        {
            _listener = new TcpListener(/*Dns.GetHostAddresses(Dns.GetHostName())[3]*/IPAddress.Parse("127.0.0.1"), 4242);
        }

        public Client AcceptClient()
        {
            _listener.Start();
            var c =  new Client(_listener.AcceptTcpClient());
            _listener.Stop();
            return c;
        }
    }
}
