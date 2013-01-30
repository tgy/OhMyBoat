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
        public static int WindowWidth;
        public static int WindowHeight;

        static public MouseState MouseState;
        static public MouseState PreviousMouseState;

        static public KeyboardState KeyboardState;
        static public KeyboardState PreviousKeyboardState;
    }
}
