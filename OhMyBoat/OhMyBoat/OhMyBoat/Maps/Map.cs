using OhMyBoat.IO;

namespace OhMyBoat.Maps
{
    public class Map
    {
        private const short Size = 10;

        public byte[,] Datas { get; set; }

        public Map(byte[,] datas)
        {
            Datas = datas;
        }

        public Map()
        {
            Datas = new byte[Size, Size];
        }

        /// <summary>
        /// Serialize the current map in a stream
        /// </summary>
        /// <param name="w">Stream writer</param>
        public void Serialize(Writer w)
        {
            w.Write(Size);

            for (byte y = 0; y < Size; y++)
                for (byte x = 0; x < Size; x++)
                    w.Write(Datas[x, y]);
        }

        /// <summary>
        /// Deserialize a map from a stream
        /// </summary>
        /// <param name="r">Stream reader</param>
        /// <returns></returns>
        public static Map Deserialize(Reader r)
        {
            var size = r.ReadInt16();
            var map = new byte[size,size];

            for (byte y = 0; y < Size; y++)
                for (byte x = 0; x < Size; x++)
                    map[x, y] = r.ReadByte();

            return new Map(map);
        }
    }
}
