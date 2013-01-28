using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OhMyBoat.Maps
{
    class Cell
    {
        public CellType Type;

        public Vector2 Position;

        public Texture2D Texture;

        public Rectangle Area;

        public Cell(int x, int y, byte b)
        {
            this.Position = new Vector2(x, y);
            this.Type = TypeFromByte(b);
        }

        public CellType TypeFromByte(byte b)
        {
            switch (b)
            {
                case 0:
                    return CellType.Water;
                case 1:
                    return CellType.HiddenBoat;
                case 2:
                    return CellType.BurningBoat;
                case 3:
                    return CellType.DestroyedBoat;
                default:
                    return CellType.Error;
            }
        }
    }
}
