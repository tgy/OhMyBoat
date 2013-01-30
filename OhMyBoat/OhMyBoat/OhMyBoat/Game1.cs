using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using OhMyBoat.Maps;

namespace OhMyBoat
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private Player _player1, _player2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.Title = "Oh My Boat! What a ballzy boat!";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = true;
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //////////////////////////////////////////////////////////

            GameDatas.GridTexture = Content.Load<Texture2D>("Grid");

            GameDatas.CellsTextures = new[]
                {
                    Content.Load<Texture2D>("WaterHidden"),
                    Content.Load<Texture2D>("Water"),
                    Content.Load<Texture2D>("BoatHidden"),
                    Content.Load<Texture2D>("BoatBurning"),
                    Content.Load<Texture2D>("BoatDestroyed")
                };

            _graphics.PreferredBackBufferWidth = GameDatas.GridTexture.Width * 2 + 50;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 200;
            _graphics.ApplyChanges();

            //////////////////////////////////////////////////////////

            _player1 = new Player("Toogy");
            _player1.Map.SetPosition(Window.ClientBounds.Width / 2 - GameDatas.GridSize, (Window.ClientBounds.Height - GameDatas.GridSize) / 2);

            _player2 = new Player("NeodyBlue");
            _player2.Map.SetPosition(Window.ClientBounds.Width / 2, (Window.ClientBounds.Height - GameDatas.GridSize) / 2);

            //////////////////////////////////////////////////////////

            GameDatas.KeyboardState = Keyboard.GetState();
            GameDatas.MouseState = Mouse.GetState();

            GameDatas.WindowWidth = Window.ClientBounds.Width;
            GameDatas.WindowHeight = Window.ClientBounds.Height;
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GameDatas.PreviousKeyboardState.IsKeyDown(Keys.F11) &&
                GameDatas.KeyboardState.IsKeyUp(Keys.F11))
                _graphics.ToggleFullScreen();

            if (GameDatas.MouseState.LeftButton == ButtonState.Released &&
                GameDatas.PreviousMouseState.LeftButton == ButtonState.Pressed)
            {
                if (_player2.Map.Area.Contains(GameDatas.MouseState.X, GameDatas.MouseState.Y))
                {
                    var x = (GameDatas.MouseState.X - _player2.Map.X - GameDatas.GridPadding) / GameDatas.CellSize;
                    var y = (GameDatas.MouseState.Y - _player2.Map.Y - GameDatas.GridPadding) / GameDatas.CellSize;
                    _player2.Play(y, x);
                }
            }

            GameDatas.PreviousKeyboardState = GameDatas.KeyboardState;
            GameDatas.PreviousMouseState = GameDatas.MouseState;
            GameDatas.KeyboardState = Keyboard.GetState();
            GameDatas.MouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            _player1.Map.Draw(_spriteBatch, true);
            _player2.Map.Draw(_spriteBatch, false);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
