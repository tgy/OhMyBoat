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

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();

            //////////////////////////////////////////////////////////

            _player1 = new Player("Toogy", Map.Generate());
            _player1.Map.Load(Content);
            _player1.Map.SetPosition(Window.ClientBounds.Width / 2 - 438, (Window.ClientBounds.Height - 438) / 2);

            _player2 = new Player("NeodyBlue", Map.Generate());
            _player2.Map.Load(Content);
            _player2.Map.SetPosition(Window.ClientBounds.Width / 2, (Window.ClientBounds.Height - 438) / 2);

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
