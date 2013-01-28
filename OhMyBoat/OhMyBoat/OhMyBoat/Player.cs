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
        public Map PlayerMap { get; set; }

        public Player(string name) : this(name, new Map())
        {
        }

        public Player(string name, Map map)
        {
            Name = name;
            PlayerMap = map;
        }
    }
}
