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

namespace Asteroids {
    /// <author>Niels Justesen, Martin Kjeldsen</author>
    /// <summary>
    /// The menu item that shows the controls. 
    /// </summary>
    class ControlsMenuItem : Selectable {

        static SpriteFont font;

        /// <summary>
        /// The contructor of ControlsMenuItem
        /// </summary>
        public ControlsMenuItem() {
        }

        /// <summary>
        /// Draws the item.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used for drawing.</param>
        public override void DrawItem(SpriteBatch spriteBatch){
            // Load highscore list
            int y = (int)Environment.BoxSize.Y+18;
            int x = (int)(Environment.GameAreaSize.X / 2 - 300);
            int x2 = x + 260;
            int width = 600;
            int height = 552;
            int divH = 18;
            spriteBatch.Draw(Textures.Px, new Rectangle(x - 12, y - 12, width + 12, height), new Color(0.1f, 0.1f, 0.1f, 1f));
            
            spriteBatch.DrawString(Font, "Player1:", new Vector2(x, y), Color.White);
            foreach(KeyValuePair<Actions,Keys> pair in Controls.Player1){
                spriteBatch.DrawString(Font, pair.Key.ToString()+":", new Vector2(x2, y), Color.White);
                spriteBatch.DrawString(Font, pair.Value.ToString(), new Vector2(x2 + 160, y), Color.White);
                y += divH;
            }
            y += divH;
            spriteBatch.DrawString(Font, "Player2:", new Vector2(x, y), Color.White);
            foreach (KeyValuePair<Actions, Keys> pair in Controls.Player2) {
                spriteBatch.DrawString(Font, pair.Key.ToString() + ":", new Vector2(x2, y), Color.White);
                spriteBatch.DrawString(Font, pair.Value.ToString(), new Vector2(x2 + 160, y), Color.White);
                y += divH;
            }
            y += divH;
            spriteBatch.DrawString(Font, "Player3:", new Vector2(x, y), Color.White);
            foreach (KeyValuePair<Actions, Keys> pair in Controls.Player3) {
                spriteBatch.DrawString(Font, pair.Key.ToString() + ":", new Vector2(x2, y), Color.White);
                spriteBatch.DrawString(Font, pair.Value.ToString(), new Vector2(x2 + 160, y), Color.White);
                y += divH;
            }
            y += divH;
            spriteBatch.DrawString(Font, "Player4:", new Vector2(x, y), Color.White);
            foreach (KeyValuePair<Actions, Keys> pair in Controls.Player4) {
                spriteBatch.DrawString(Font, pair.Key.ToString() + ":", new Vector2(x2, y), Color.White);
                spriteBatch.DrawString(Font, pair.Value.ToString(), new Vector2(x2 + 160, y), Color.White);
                y += divH;
            }
            y += divH;
            spriteBatch.DrawString(Font, "Spawn:", new Vector2(x, y), Color.White);
            spriteBatch.DrawString(Font, "Shoot and activate shield", new Vector2(x2, y), Color.White);
            y += divH;
            spriteBatch.DrawString(Font, "Restart game:", new Vector2(x, y), Color.White);
            spriteBatch.DrawString(Font, "F2", new Vector2(x2, y), Color.White);
            y += divH;
            spriteBatch.DrawString(Font, "Music on/off:", new Vector2(x, y), Color.White);
            spriteBatch.DrawString(Font, "F10", new Vector2(x2, y), Color.White);
            y += divH;
            spriteBatch.DrawString(Font, "Back in menu:", new Vector2(x, y), Color.White);
            spriteBatch.DrawString(Font, "Esc or F6", new Vector2(x2, y), Color.White);
            y += divH;
            spriteBatch.DrawString(Font, "End game:", new Vector2(x, y), Color.White);
            spriteBatch.DrawString(Font, "F1", new Vector2(x2, y), Color.White);

        }

        /// <summary>
        /// This item takes no actions.
        /// </summary>
        public override void TakeAction() {
            
        }

        /// <summary>
        /// This item cannot be pressed
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
    }
}
