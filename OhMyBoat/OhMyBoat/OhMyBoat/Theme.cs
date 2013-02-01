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
    public class Theme
    {
        public string Name;

        public SpriteFont ReturnFont;

        public Texture2D[] CellsTextures;
        public Texture2D GridTexture, AimTexture, LogoTexture;

        public short CellsNumber, CellSize, GridPadding, GridSize, AimPadding;

        public Theme(string name, short cellsNumber, short cellSize, short gridPadding, short aimPadding)
        {
            Name = name;
            CellsNumber = cellsNumber;
            CellSize = cellSize;
            GridPadding = gridPadding;
            GridSize = (short) (cellSize*CellsNumber + gridPadding*2);
            AimPadding = aimPadding;
        }

        public void Load(ContentManager content)
        {
            ReturnFont = content.Load<SpriteFont>("Themes/" + Name + "/Return");

            GridTexture = content.Load<Texture2D>("Themes/" + Name + "/Grid");

            AimTexture = content.Load<Texture2D>("Themes/" + Name + "/Aim");

            LogoTexture = content.Load<Texture2D>("Themes/" + Name + "/Logo");

            CellsTextures = new[]
                {
                    content.Load<Texture2D>("Themes/" + Name + "/WaterHidden"),
                    content.Load<Texture2D>("Themes/" + Name + "/Water"),
                    content.Load<Texture2D>("Themes/" + Name + "/BoatHidden"),
                    content.Load<Texture2D>("Themes/" + Name + "/BoatBurning"),
                    content.Load<Texture2D>("Themes/" + Name + "/BoatDestroyed")
                };
        }
    }
}
