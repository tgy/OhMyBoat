using OhMyBoat.IO;

namespace OhMyBoat.Maps
{
    public class Map
    {
        public const short Size = 10;

        public byte[,] Datas { get; set; }

        public Map(byte[,] datas)
        {
            Datas = datas;
        }

        public Map()
        {
            Datas = new byte[Size, Size];
        }
    }
}
