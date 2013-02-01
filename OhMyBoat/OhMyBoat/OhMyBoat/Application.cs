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
using OhMyBoat.Menu;
using OhMyBoat.Network;
using System.Net.Sockets;

namespace OhMyBoat
{
    public class Application : Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private Client _client;

        private Stack<GameState> _gameStates; 

        public Application(bool isServer, string serverIp = "")
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.Title = "Oh My Boat! What a ballzy boat!";

            if (isServer)
                _client = new Server().AcceptClient();
            else
            {
                var client = new TcpClient();
                client.Connect(serverIp, 4242);
                _client = new Client(client);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = true;

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _gameStates = new Stack<GameState>();

            //////////////////////////////////////////////////////////

            GameDatas.GridTheme = new GridTheme("PinkTheme", 10, 40, 17, 3);
            
            GameDatas.GridTheme.Load(Content);

            //////////////////////////////////////////////////////////

            _graphics.PreferredBackBufferWidth = GameDatas.GridTheme.GridTexture.Width * 2 + 50;
            _graphics.PreferredBackBufferHeight = GameDatas.GridTheme.GridTexture.Height + GameDatas.GridTheme.LogoTexture.Height + 50;
            _graphics.ApplyChanges();

            GameDatas.WindowWidth = Window.ClientBounds.Width;
            GameDatas.WindowHeight = Window.ClientBounds.Height;

            //////////////////////////////////////////////////////////
            
            var playState = new PlayState();

            _gameStates.Push(playState);
            _gameStates.Peek().Initialize();
            _gameStates.Peek().LoadContent();
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            GameDatas.KeyboardState = Keyboard.GetState();
            GameDatas.MouseState = Mouse.GetState();

            //////////////////////////////////////////////////////////

            _gameStates.Peek().Update(gameTime);

            //////////////////////////////////////////////////////////

            GameDatas.PreviousKeyboardState = GameDatas.KeyboardState;
            GameDatas.PreviousMouseState = GameDatas.MouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            //////////////////////////////////////////////////////////

            _gameStates.Peek().Draw(_spriteBatch);

            //////////////////////////////////////////////////////////

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
