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
        private enum State
        {
            Water,
            HiddenBoat,
            BurningBoat,
            DestroyedBoat,
            Error
        }

        public State state;

        public Vector2 position;

        public Texture2D texture;

        public Rectangle area;

        public Cell(int x, int y, byte b)
        {
            this.position = new Vector2(x, y);
            this.state = StateFromByte(b);
        }

        public State StateFromByte(byte b)
        {
            switch (b)
            {
                case 0:
                    return State.Water;
                case 1:
                    return State.HiddenBoat;
                case 2:
                    return State.BurningBoat;
                case 3:
                    return State.DestroyedBoat;
                default:
                    return State.Error;
            }
        }
    }
}
