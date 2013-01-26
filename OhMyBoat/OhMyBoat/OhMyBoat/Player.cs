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

        /// <summary>
        /// Serialize the current player in a stream
        /// </summary>
        /// <param name="w">Stream writer</param>
        public void Serialize(Writer w)
        {
            w.Write(Name);
            PlayerMap.Serialize(w);
        }

        /// <summary>
        /// Deserialize a player from a stream
        /// </summary>
        /// <param name="r">Stream reader</param>
        /// <returns></returns>
        public static Player Deserialize(Reader r)
        {
            string name = r.ReadString();
            Map map = Map.Deserialize(r);

            return new Player(name, map);
        }
    }
}
