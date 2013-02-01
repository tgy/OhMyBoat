using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OhMyBoat.Maps;

namespace OhMyBoat.IO
{
    public class Writer : BinaryWriter
    {
        public Writer(Stream stream) : base(stream)
        {
        }

        public void WriteMap(Map map)
        {
            Write(GameDatas.GridTheme.CellsNumber);

            for (byte y = 0; y < GameDatas.GridTheme.CellsNumber; y++)
                for (byte x = 0; x < GameDatas.GridTheme.CellsNumber; x++)
                    Write(map.Datas[x, y]);
        }

        public void WritePlayer(Player player)
        {
            Write(player.Name);
            WriteMap(player.Map);
        }
    }
}
