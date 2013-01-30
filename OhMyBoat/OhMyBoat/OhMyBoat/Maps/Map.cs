using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OhMyBoat.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OhMyBoat.Maps
{
    public class Map
    {
        public byte[,] Datas;

        public int Size;

        private Texture2D[] _cellsTextures;

        private Texture2D _gridTexture;

        public int X, Y;

        public Rectangle Area;

        public Map(byte[,] d)
        {
            Datas = d;
        }

        public void SetPosition(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            
            Area = new Rectangle(X, Y, 438, 438);
        }

        public void Load(ContentManager content)
        {
            _gridTexture = content.Load<Texture2D>("Grid");

            _cellsTextures = new[]
                {
                    content.Load<Texture2D>("WaterHidden"),
                    content.Load<Texture2D>("Water"),
                    content.Load<Texture2D>("BoatHidden"),
                    content.Load<Texture2D>("BoatBurning"),
                    content.Load<Texture2D>("BoatDestroyed")
                };
        }

        public void Draw(SpriteBatch spriteBatch, bool show)
        {
            spriteBatch.Draw(_gridTexture, new Vector2(X, Y), Color.White);

            int x = X + 19;
            int y = Y + 19;

            for (var i = 0; i < 10; i++)
                for (var j = 0; j < 10; j++)
                {
                    if (show)
                    {
                        switch (Datas[i, j])
                        {
                            case (byte)CellState.WaterHidden:
                                spriteBatch.Draw(_cellsTextures[(byte)CellState.Water], new Rectangle(x + j * 40, y + i * 40, 40, 40), Color.White);
                                break;
                            case (byte)CellState.BoatHidden:
                                spriteBatch.Draw(_cellsTextures[(byte)CellState.BoatDestroyed], new Rectangle(x + j * 40, y + i * 40, 40, 40), Color.White);
                                break;
                            default:
                                spriteBatch.Draw(_cellsTextures[Datas[i, j]], new Rectangle(x + j * 40, y + i * 40, 40, 40), Color.White);
                                break;
                        }
                    }

                    else
                        spriteBatch.Draw(_cellsTextures[Datas[i, j]], new Rectangle(x + j*40, y + i*40, 40, 40), Color.White);
                }
        }

        public static Map Generate()
        {
            var datas = new byte[10, 10];

            var boats = new List<int> {7, 5, 3, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2};

            var vertical = false;

            foreach (var boat in boats)
            {
                #region tests

                int x;
                int y;
                if (vertical)
                {
                    bool ok;

                    do
                    {
                        ok = true;
                        x = Random10();
                        y = Random10();

                        if (datas[x, y] == (byte) CellState.BoatHidden)
                        {
                            ok = false;
                            continue;
                        }

                        for (var i = x - 1; i < x + boat + 1; i++)
                        {
                            if (i < 0 || i > 9)
                                continue;

                            if (datas[i, y] == (byte)CellState.BoatHidden)
                                ok = false;
                        }

                        if (y - 1 >= 0 && y - 1 <= 9)
                        {

                            for (var i = x; i < x + boat; i++)
                            {
                                if (i < 0 || i > 9)
                                    continue;

                                if (datas[i, y - 1] == (byte)CellState.BoatHidden)
                                    ok = false;
                            }
                        }

                        if (y + 1 >= 0 && y + 1 <= 9)
                        {

                            for (var i = x; i < x + boat; i++)
                            {
                                if (i < 0 || i > 9)
                                    continue;

                                if (datas[i, y + 1] == (byte)CellState.BoatHidden)
                                    ok = false;
                            }
                        }

                    } while (x + boat - 1 >= 10 || !ok);
                    
                    for (var i = x; i < x + boat; i++)
                    {
                        datas[i, y] = (byte) CellState.BoatHidden;
                    }
                }

                else
                {
                    bool ok;

                    do
                    {
                        ok = true;
                        x = Random10();
                        y = Random10();

                        if (datas[x, y] == (byte) CellState.BoatHidden)
                        {
                            ok = false;
                            continue;
                        }

                        for (var i = y - 1; i < y + boat + 1; i++)
                        {
                            if (i < 0 || i > 9)
                                continue;

                            if (datas[x, i] == (byte)CellState.BoatHidden)
                                ok = false;
                        }

                        if (x - 1 >= 0 && x - 1 <= 9)
                        {

                            for (var i = y; i < y + boat; i++)
                            {
                                if (i < 0 || i > 9)
                                    continue;

                                if (datas[x - 1, i] == (byte)CellState.BoatHidden)
                                    ok = false;
                            }

                        }

                        if (x + 1 >= 0 && x + 1 <= 9)
                        {

                            for (var i = y; i < y + boat; i++)
                            {
                                if (i < 0 || i > 9)
                                    continue;

                                if (datas[x + 1, i] == (byte)CellState.BoatHidden)
                                    ok = false;
                            }

                        }

                    } while (y + boat - 1 >= 10 || !ok);

                    for (var i = y; i < y + boat; i++)
                    {
                        datas[x, i] = (byte)CellState.BoatHidden;
                    }
                }

                #endregion

                vertical = !vertical;
            }

            return new Map(datas);
        }
        
        static private byte Random10()
        {
            var b = new byte[1];

            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);

            return (byte) (b[0] % 10);
        }

    }
}
