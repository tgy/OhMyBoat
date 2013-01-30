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

        public string Play(int x, int y)
        {
            if (!Shoot(x, y))
            {
                return "You missed the shot... I shall not say that you deeply are a sucker.";
            }

            if (Sink(x, y))
            {
                Achieve(x, y);
                return "You hit the target and you destroyed a boat! You can be pride of yourself.";
            }

            return "You hit the target! Well done!";
        }

        public bool IsOver()
        {
            for (var i = 0; i < 10; i++)
                for (var j = 0; j < 10; j++)
                    if (Map.Datas[i, j] == (byte) CellState.BoatHidden)
                        return false;

            return true;
        }

        public bool Shoot(int x, int y)
        {
            switch (Map.Datas[x, y])
            {
                case (byte)CellState.BoatHidden:
                    Shots++;
                    Map.Datas[x, y] = (byte)CellState.BoatBurning;
                    return true;
                case (byte)CellState.WaterHidden:
                    Shots++;
                    Map.Datas[x, y] = (byte)CellState.Water;
                    return false;
                default:
                    return false;
            }
        }

        public bool Sink(int x, int y)
        {
            return Sink(x - 1, y, -1, 0) && Sink(x + 1, y, 1, 0) && Sink(x, y - 1, 0, -1) && Sink(x, y + 1, 0, 1);
        }

        public bool Sink(int x, int y, int dirx, int diry)
        {
            return (x < 0 || y < 0 || x > 9 || y > 9 || Map.Datas[x, y] == (byte)CellState.WaterHidden || Map.Datas[x, y] == (byte)CellState.Water) || (Map.Datas[x, y] == (byte)CellState.BoatBurning && Sink(x + dirx, y + diry, dirx, diry));
        }

        public void Achieve(int x, int y)
        {
            Achieve(x, y, 0, 0);
            Achieve(x - 1, y, -1, 0);
            Achieve(x + 1, y, 1, 0);
            Achieve(x, y - 1, 0, -1);
            Achieve(x, y + 1, 0, 1);
        }

        public bool Achieve(int x, int y, int dirx, int diry)
        {
            if (x < 0 || y < 0 || x > 9 || y > 9 || Map.Datas[x, y] == (byte)CellState.WaterHidden || Map.Datas[x, y] == (byte)CellState.Water)
            {
                return true;
            }

            if (Map.Datas[x, y] == (byte) CellState.BoatBurning && (dirx == diry || Achieve(x + dirx, y + diry, dirx, diry)))
            {
                if (x - 1 >= 0 && Map.Datas[x - 1, y] == (byte) CellState.WaterHidden)
                {
                    Map.Datas[x - 1, y] = (byte) CellState.Water;
                }

                if (x + 1 < 10 && Map.Datas[x + 1, y] == (byte)CellState.WaterHidden)
                {
                    Map.Datas[x + 1, y] = (byte) CellState.Water;
                }

                if (y - 1 >= 0 && Map.Datas[x, y - 1] == (byte)CellState.WaterHidden)
                {
                    Map.Datas[x, y - 1] = (byte) CellState.Water;
                }

                if (y + 1 < 10 && Map.Datas[x, y + 1] == (byte)CellState.WaterHidden)
                {
                    Map.Datas[x, y + 1] = (byte) CellState.Water;
                }

                return true;
            }

            return false;
        }
    }
}
