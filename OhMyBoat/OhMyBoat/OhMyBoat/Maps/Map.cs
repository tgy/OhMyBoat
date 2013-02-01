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

        public int X, Y;

        public Point Aim;

        public int AimPeriod;

        public Rectangle Area;

        public Map(byte[,] d)
        {
            Datas = d;
        }

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;

            Area = new Rectangle(x + GameDatas.GridTheme.GridPadding, y + GameDatas.GridTheme.GridPadding, GameDatas.GridTheme.GridSize - 2*GameDatas.GridTheme.GridPadding, GameDatas.GridTheme.GridSize - 2*GameDatas.GridTheme.GridPadding);
        }

        public void Draw(SpriteBatch spriteBatch, bool show)
        {
            spriteBatch.Draw(GameDatas.GridTheme.GridTexture, new Vector2(X, Y), Color.White);

            var x = X + GameDatas.GridTheme.GridPadding;
            var y = Y + GameDatas.GridTheme.GridPadding;

            for (var i = 0; i < GameDatas.GridTheme.CellsNumber; i++)
                for (var j = 0; j < GameDatas.GridTheme.CellsNumber; j++)
                {
                    if (show)
                    {
                        switch (Datas[i, j])
                        {
                            case (byte) CellState.WaterHidden:
                                spriteBatch.Draw(GameDatas.GridTheme.CellsTextures[(byte) CellState.Water],
                                                 new Rectangle(x + j*GameDatas.GridTheme.CellSize,
                                                               y + i*GameDatas.GridTheme.CellSize,
                                                               GameDatas.GridTheme.CellSize,
                                                               GameDatas.GridTheme.CellSize),
                                                 Color.FromNonPremultiplied(255, 255, 255, 100));
                                break;
                            case (byte) CellState.BoatBurning:
                                spriteBatch.Draw(GameDatas.GridTheme.CellsTextures[(byte) CellState.BoatDestroyed],
                                                 new Rectangle(x + j*GameDatas.GridTheme.CellSize,
                                                               y + i*GameDatas.GridTheme.CellSize,
                                                               GameDatas.GridTheme.CellSize,
                                                               GameDatas.GridTheme.CellSize), Color.White);
                                break;
                            case (byte) CellState.BoatHidden:
                                spriteBatch.Draw(GameDatas.GridTheme.CellsTextures[(byte) CellState.BoatDestroyed],
                                                 new Rectangle(x + j*GameDatas.GridTheme.CellSize,
                                                               y + i*GameDatas.GridTheme.CellSize,
                                                               GameDatas.GridTheme.CellSize,
                                                               GameDatas.GridTheme.CellSize),
                                                 Color.FromNonPremultiplied(255, 255, 255, 100));
                                break;
                            default:
                                spriteBatch.Draw(GameDatas.GridTheme.CellsTextures[Datas[i, j]],
                                                 new Rectangle(x + j*GameDatas.GridTheme.CellSize,
                                                               y + i*GameDatas.GridTheme.CellSize,
                                                               GameDatas.GridTheme.CellSize,
                                                               GameDatas.GridTheme.CellSize), Color.White);
                                break;
                        }
                    }

                    else
                    {
                        spriteBatch.Draw(GameDatas.GridTheme.CellsTextures[Datas[i, j]],
                                         new Rectangle(x + j*GameDatas.GridTheme.CellSize,
                                                       y + i*GameDatas.GridTheme.CellSize, GameDatas.GridTheme.CellSize,
                                                       GameDatas.GridTheme.CellSize), Color.White);
                    }
                }

            if (!show)
                spriteBatch.Draw(GameDatas.GridTheme.AimTexture,
                                 new Rectangle(x + Aim.X*GameDatas.GridTheme.CellSize - GameDatas.GridTheme.AimPadding,
                                               y + Aim.Y*GameDatas.GridTheme.CellSize - GameDatas.GridTheme.AimPadding,
                                               GameDatas.GridTheme.AimTexture.Width,
                                               GameDatas.GridTheme.AimTexture.Height),
                                 Color.White);
        }

        static public Map Generate()
        {
            var datas = new byte[GameDatas.GridTheme.CellsNumber, GameDatas.GridTheme.CellsNumber];
            var boats = new List<int> {4, 3, 3, 2, 2, 2, 1, 1, 1, 1};
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
                        x = RandomCell();
                        y = RandomCell();

                        if (datas[x, y] == (byte) CellState.BoatHidden)
                        {
                            ok = false;
                            continue;
                        }

                        for (var i = x - 1; i < x + boat + 1; i++)
                        {
                            if (i < 0 || i > (GameDatas.GridTheme.CellsNumber - 1))
                                continue;

                            if (datas[i, y] == (byte)CellState.BoatHidden)
                                ok = false;
                        }

                        if (y - 1 >= 0 && y - 1 < GameDatas.GridTheme.CellsNumber)
                        {
                            for (var i = x; i < x + boat; i++)
                            {
                                if (i < 0 || i > (GameDatas.GridTheme.CellsNumber - 1))
                                    continue;

                                if (datas[i, y - 1] == (byte)CellState.BoatHidden)
                                    ok = false;
                            }
                        }

                        if (y + 1 >= 0 && y + 1 < GameDatas.GridTheme.CellsNumber)
                        {
                            for (var i = x; i < x + boat; i++)
                            {
                                if (i < 0 || i > (GameDatas.GridTheme.CellsNumber - 1))
                                    continue;

                                if (datas[i, y + 1] == (byte)CellState.BoatHidden)
                                    ok = false;
                            }
                        }

                    } while (x + boat - 1 >= GameDatas.GridTheme.CellsNumber || !ok);
                    
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
                        x = RandomCell();
                        y = RandomCell();

                        if (datas[x, y] == (byte) CellState.BoatHidden)
                        {
                            ok = false;
                            continue;
                        }

                        for (var i = y - 1; i < y + boat + 1; i++)
                        {
                            if (i < 0 || i > (GameDatas.GridTheme.CellsNumber - 1))
                                continue;

                            if (datas[x, i] == (byte)CellState.BoatHidden)
                                ok = false;
                        }

                        if (x - 1 >= 0 && x - 1 < GameDatas.GridTheme.CellsNumber)
                        {

                            for (var i = y; i < y + boat; i++)
                            {
                                if (i < 0 || i > (GameDatas.GridTheme.CellsNumber - 1))
                                    continue;

                                if (datas[x - 1, i] == (byte)CellState.BoatHidden)
                                    ok = false;
                            }

                        }

                        if (x + 1 >= 0 && x + 1 < GameDatas.GridTheme.CellsNumber)
                        {

                            for (var i = y; i < y + boat; i++)
                            {
                                if (i < 0 || i > (GameDatas.GridTheme.CellsNumber - 1))
                                    continue;

                                if (datas[x + 1, i] == (byte)CellState.BoatHidden)
                                    ok = false;
                            }

                        }

                    } while (y + boat - 1 >= GameDatas.GridTheme.CellsNumber || !ok);

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
        
        static private byte RandomCell()
        {
            var b = new byte[1];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            return (byte) (b[0] % GameDatas.GridTheme.CellsNumber);
        }

    }
}
