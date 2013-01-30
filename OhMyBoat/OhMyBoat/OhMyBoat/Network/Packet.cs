using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OhMyBoat.IO;

namespace OhMyBoat.Network
{
    public class Packet
    {
        public const byte HeaderSize = 2; // un byte opcode et un byte de taille

        public static PacketHeader GetHeader(Reader r)
        {
            return new PacketHeader(r.ReadByte(), r.ReadByte());
        }
    }
}
