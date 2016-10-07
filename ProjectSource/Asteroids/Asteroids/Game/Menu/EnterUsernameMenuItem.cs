﻿using System;
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

namespace Asteroids {
    /// <author>Niels Justesen</author>
    /// <summary>
    /// The menu item used for entering a username to create a new user.
    /// </summary>
    class EnterUsernameMenuItem : Selectable {
        static SpriteFont font;
        static int maxLength = 13;
        String text;
        List<Keys> keysDown = new List<Keys>();
        List<Keys> keysUp = new List<Keys>();
        List<Keys> validChars = new List<Keys>() {
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L,
            Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.X, Keys.Y,
            Keys.Z, Keys.W,
        };

        Vector2 position;
        Vector2 positionText;
        Vector2 positionHeader;

        /// <summary>
        /// The constructor of EnterUsernameMenuItem.
        /// </summary>
        public EnterUsernameMenuItem() {
            text = "";
            positionHeader = new Vector2((Environment.GameAreaSize.X / 2) - 140 + 8, Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2) - 190);
            position = new Vector2((Environment.GameAreaSize.X / 2) - 151, (Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2)) - 165);
            positionText = new Vector2((Environment.GameAreaSize.X / 2) - 140 + 18, Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2) - 140);
        }

        /// <summary>
        /// Register new characters typed.
        /// </summary>
        public override void TakeAction() {
            keysUp.Clear();
            foreach (Keys k in keysDown) {
                if (!Keyboard.GetState().IsKeyDown(k)) {
                    keysUp.Add(k);
                }
            }
            foreach (Keys k in keysUp) {
                keysDown.Remove(k);
            }
            if (Menu.CurrentScreen.CurrentItem.Equals(this)) {
                if (Keyboard.GetState().IsKeyDown(Keys.Back) && !keysDown.Contains(Keys.Back) && text.Length>0) {
                    text = text.Remove(text.Length-1);
                    keysDown.Add(Keys.Back);
                }
                foreach (Keys k in Keyboard.GetState().GetPressedKeys()) {
                    if (!keysDown.Contains(k) && validChars.Contains(k) && text.Length<maxLength) {
                        Char[] chars = k.ToString().ToLowerInvariant().ToCharArray();
                        if (chars.Length > 1) {
                            text += chars[1];
                        } else {
                            text += chars[0];
                        }
                        
                        if (text.Length > 2) {
                            Login.NewUsernameToTry = text;
                        }
                        keysDown.Add(k);
                    }
                }
            }
        }

        /// <summary>
        /// This item cannot be pressed.
        /// </summary>
        public override void Pressed() {
        
        }

        /// <summary>
        /// The font used in this item.
        /// </summary>
        public static SpriteFont Font {
            get { return font; }
            set { font = value; }
        }

        /// <summary>
        /// Draws the item. If the text is invalid, it turns red.
        /// </summary>
        /// <param name="spriteBatch">The spriebatch used for drawing.</param>
        public override void DrawItem(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(font, "Username (min. 3 characters)", positionHeader, Color.White);
            Color color = Color.White;
            if (text.Length < 2) {
                color = Color.Red;
            }
            if (Menu.CurrentScreen.CurrentItem.Equals(this)) {
                spriteBatch.Draw(Textures.TextFieldChosen, position, color);
                spriteBatch.DrawString(font, text.ToUpperInvariant(), positionText, color);
            } else {
                spriteBatch.Draw(Textures.TextField, position, color);
                spriteBatch.DrawString(font, text.ToUpperInvariant(), positionText, color);
            }
        }
    }
}