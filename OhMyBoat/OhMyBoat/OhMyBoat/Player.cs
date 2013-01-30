using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OhMyBoat.IO;
using OhMyBoat.Maps;

namespace OhMyBoat
{
    public class Player
    {
        public string Name { get; private set; }

        public int Shots;

        public Map Map { get; set; }

        public Player(string name) : this(name, Map.Generate()) { }

        public Player(string name, Map map)
        {
            Name = name;
            Map = map;
            Shots = 0;
        }

        public void Play(Player player, int x, int y)
        {
            switch (Map.Datas[x, y])
            {
                case (byte) CellState.BoatHidden:
                    Shots++;
                    Map.Datas[x, y] = (byte) CellState.BoatBurning;
                    break;
                case (byte) CellState.WaterHidden:
                    Shots++;
                    Map.Datas[x, y] = (byte) CellState.Water;
                    break;
            }
        }
    }
}
