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

        public Server(string ip, int port)
        {
            _listener = new TcpListener(IPAddress.Parse(ip), port);
        }

        public void StartListening()
        {
            new Client(_listener.AcceptTcpClient());
        }
    }
}
