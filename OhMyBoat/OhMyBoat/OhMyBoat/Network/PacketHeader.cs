using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OhMyBoat.Network
{
    public class PacketHeader
    {
        public byte OpCode { get; private set; }
        public byte DataSize { get; private set; }
        public byte TotalSize { get { return (byte) (DataSize + Packet.HeaderSize); } }

        public PacketHeader(byte opcode, byte datasize)
        {
            OpCode = opcode;
            DataSize = datasize;
        }
    }
}
