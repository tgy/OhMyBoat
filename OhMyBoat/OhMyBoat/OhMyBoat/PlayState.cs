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
    class PlayState : GameState
    {
        private Player _player1, _player2;

        public override void LoadContent()
        {
            _player1 = new Player("Toogy");
            _player1.Map.SetPosition(GameDatas.WindowWidth/2 - GameDatas.Theme.GridSize,
                                        GameDatas.WindowHeight - GameDatas.Theme.GridSize - 25);

            _player2 = new Player("NeodyBlue");
            _player2.Map.SetPosition(GameDatas.WindowWidth/2,
                                        GameDatas.WindowHeight - GameDatas.Theme.GridSize - 25);
        }

        public override void Update(GameTime gameTime)
        {
            if (GameDatas.KeyboardFocus &&
                (GameDatas.MouseState.X != GameDatas.PreviousMouseState.X ||
                    GameDatas.MouseState.Y != GameDatas.PreviousMouseState.Y))
            {
                if (_player1.Map.Area.Contains(GameDatas.MouseState.X, GameDatas.MouseState.Y) ||
                    _player2.Map.Area.Contains(GameDatas.MouseState.X, GameDatas.MouseState.Y))
                    GameDatas.KeyboardFocus = false;
            }

            if (!GameDatas.KeyboardFocus &&
                (GameDatas.KeyboardState.IsKeyDown(Keys.Right) || GameDatas.KeyboardState.IsKeyDown(Keys.Down) ||
                    GameDatas.KeyboardState.IsKeyDown(Keys.Up) || (GameDatas.KeyboardState.IsKeyDown(Keys.Down))))
                GameDatas.KeyboardFocus = true;

            _player1.Update();
            _player2.Update();

            if ((GameDatas.PreviousKeyboardState.IsKeyDown(Keys.Enter) && GameDatas.KeyboardState.IsKeyUp(Keys.Enter)) ||
                (!GameDatas.KeyboardFocus && GameDatas.MouseState.LeftButton == ButtonState.Released &&
                 GameDatas.PreviousMouseState.LeftButton == ButtonState.Pressed))
            {
                _player2.Play(_player2.Map.Aim.Y, _player2.Map.Aim.X);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameDatas.Theme.LogoTexture,
                             new Rectangle((GameDatas.WindowWidth - GameDatas.Theme.LogoTexture.Width)/2, 15,
                                           GameDatas.Theme.LogoTexture.Width, GameDatas.Theme.LogoTexture.Height),
                             Color.White);

            _player1.Map.Draw(spriteBatch, true);
            _player2.Map.Draw(spriteBatch, false);
        }
    }
}
