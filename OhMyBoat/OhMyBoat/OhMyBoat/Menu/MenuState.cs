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

namespace OhMyBoat.Menu
{
    class MenuState : GameState
    {
        private readonly List<MenuItem> _items;
        private int _selectedItem;

        private readonly bool _loop;
        private int _menuPeriod;

        public MenuState(List<MenuItem> items, bool loop, int menuPeriod)
        {
            _items = items;
            _loop = loop;
            _menuPeriod = menuPeriod;
        }

        public override void Update(GameTime gameTime)
        {
            if (GameDatas.KeyboardFocus &&
                (GameDatas.MouseState.X != GameDatas.PreviousMouseState.X ||
                 GameDatas.MouseState.Y != GameDatas.PreviousMouseState.Y))
                foreach (var item in _items.Where(item => item.Area.Contains(GameDatas.MouseState.X, GameDatas.MouseState.Y)))
                    GameDatas.KeyboardFocus = false;

            if (!GameDatas.KeyboardFocus && (GameDatas.KeyboardState.GetPressedKeys().Length > 0))
                GameDatas.KeyboardFocus = true;

            if (GameDatas.KeyboardFocus)
            {
                _items[_selectedItem].Focused = false;

                if (GameDatas.KeyboardState.IsKeyDown(Keys.Up) && (GameDatas.PreviousKeyboardState.IsKeyUp(Keys.Up) || _menuPeriod == GameDatas.MenuPeriod))
                {
                    if (_selectedItem - 1 >= 0 && _items[_selectedItem - 1].NoClick)
                        _selectedItem = _selectedItem - 2;
                    else
                        _selectedItem--;
                    _menuPeriod = 0;
                }

                if (GameDatas.KeyboardState.IsKeyDown(Keys.Down) && (GameDatas.PreviousKeyboardState.IsKeyUp(Keys.Down) || _menuPeriod == GameDatas.MenuPeriod))
                {
                    if (_selectedItem + 1 < _items.Count && _items[_selectedItem + 1].NoClick)
                        _selectedItem = _selectedItem + 2;
                    else
                        _selectedItem++;
                    _menuPeriod = 0;
                }

                if (_loop)
                {
                    if (_selectedItem < 0)
                        _selectedItem = _items.Count - 1;
                    else if (_selectedItem >= _items.Count)
                        _selectedItem = _items.IndexOf(_items.Find(x => x.NoClick == false));
                }

                else
                {
                    if (_selectedItem < 0)
                        _selectedItem = _items.IndexOf(_items.Find(x => x.NoClick == false));
                    else if (_selectedItem >= _items.Count)
                        _selectedItem = _items.Count - 1;
                }

                _items[_selectedItem].Focused = true;

                _items[_selectedItem].Update(gameTime);
            }

            else
            {
                for (var i = 0; i < _items.Count; i++)
                {
                    if (!_items[i].Area.Contains(GameDatas.MouseState.X, GameDatas.MouseState.Y)) continue;
                    _items[_selectedItem].Focused = false;
                    _selectedItem = i;
                    _items[_selectedItem].Focused = true;
                }
            }
        }
    }
}
