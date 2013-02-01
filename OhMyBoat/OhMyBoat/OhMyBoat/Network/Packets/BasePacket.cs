using OhMyBoat.IO;

namespace OhMyBoat.Network.Packets
{
    public abstract class BasePacket
    {
        public abstract byte OpCode { get; }

        public abstract void Unpack(Client client, Packet packet);

        public abstract void Pack(Client client, object data);
    }
}
