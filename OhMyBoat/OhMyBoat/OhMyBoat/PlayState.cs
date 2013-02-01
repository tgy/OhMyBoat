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
using OhMyBoat.Network;
using OhMyBoat.Network.Events;
using OhMyBoat.Network.Packets;

namespace OhMyBoat
{
    class PlayState : GameState
    {
        private Player _current, _enemy;
        private Client _client;

        public PlayState(Client client)
        {
            _client = client;
        }

        public override void Initialize()
        {
            base.Initialize();
            _current = new Player("Toogy");
            _current.Map.SetPosition(GameDatas.WindowWidth / 2 - GameDatas.GridTheme.GridSize,
                                        GameDatas.WindowHeight - GameDatas.GridTheme.GridSize - 25);
            Parser.RegisterPackets(ManageNetworkEvents);

            SendCurrentPlayer();
        }

        void SendCurrentPlayer()
        {
            new BasicsDatasPacket().Pack(_client, Maps.Map.Generate(), _current.Name);
        }

        public void ManageNetworkEvents(NetworkEvent eventDatas)
        {
            switch (eventDatas.PacketOpCode)
            {
                case 1:
                    var basicsDatas = eventDatas as BasicsDatasEvent;
                    _current.Map = basicsDatas.EnemyMap;
                    _enemy = new Player(basicsDatas.Enemy);
                    return;
            }
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (_enemy == null) // pas encore connecté
                return;

            if (GameDatas.KeyboardFocus &&
                (GameDatas.MouseState.X != GameDatas.PreviousMouseState.X ||
                    GameDatas.MouseState.Y != GameDatas.PreviousMouseState.Y))
            {
                if (_current.Map.Area.Contains(GameDatas.MouseState.X, GameDatas.MouseState.Y) ||
                    _enemy.Map.Area.Contains(GameDatas.MouseState.X, GameDatas.MouseState.Y))
                    GameDatas.KeyboardFocus = false;
            }

            if (!GameDatas.KeyboardFocus &&
                (GameDatas.KeyboardState.IsKeyDown(Keys.Right) || GameDatas.KeyboardState.IsKeyDown(Keys.Down) ||
                    GameDatas.KeyboardState.IsKeyDown(Keys.Up) || (GameDatas.KeyboardState.IsKeyDown(Keys.Down))))
                GameDatas.KeyboardFocus = true;

            _current.Update();
            _enemy.Update();

            if ((GameDatas.PreviousKeyboardState.IsKeyDown(Keys.Enter) && GameDatas.KeyboardState.IsKeyUp(Keys.Enter)) || (!GameDatas.KeyboardFocus && GameDatas.MouseState.LeftButton == ButtonState.Released && GameDatas.PreviousMouseState.LeftButton == ButtonState.Pressed))
            {
                _enemy.Play(_enemy.Map.Aim.Y, _enemy.Map.Aim.X);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_enemy == null) // pas encore connecté
                return;

            spriteBatch.Draw(GameDatas.GridTheme.LogoTexture, new Rectangle((GameDatas.WindowWidth - GameDatas.GridTheme.LogoTexture.Width) / 2, 15, GameDatas.GridTheme.LogoTexture.Width, GameDatas.GridTheme.LogoTexture.Height), Color.White);

            _current.Map.Draw(spriteBatch, true);
            _enemy.Map.Draw(spriteBatch, false);
        }
    }
}
