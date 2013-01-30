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

namespace OhMyBoat
{
    static class GameDatas
    {
        static public int WindowWidth;
        static public int WindowHeight;

        static public Texture2D[] CellsTextures;
        public static Texture2D GridTexture;

        public const int CellSize = 40;
        public const int GridPadding = 19;
        public const int GridSize = CellSize * 10 + GridPadding * 2;

        static public MouseState MouseState;
        static public MouseState PreviousMouseState;

        static public KeyboardState KeyboardState;
        static public KeyboardState PreviousKeyboardState;
    }
}
