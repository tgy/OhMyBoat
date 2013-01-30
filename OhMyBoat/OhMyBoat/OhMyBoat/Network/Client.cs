using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace OhMyBoat.Network
{
    public class Client
    {
        private readonly TcpClient _client;
        public bool Connected { get; set; }

        public Stream Stream
        {
            get 
            {
                return Connected ? _client.GetStream() : Stream.Null;
            }
        }

        public Client(TcpClient client)
        {
            _client = client;
            Connected = true;

            new System.Threading.Tasks.Task(Receive).Start();
        }

        void Receive()
        {
            while (Connected)
            {
                //get headers
                //read packet
                //check if disconnected
            }
        }
    }
}
