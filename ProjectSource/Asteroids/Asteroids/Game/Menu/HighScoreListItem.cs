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
    /// <author>Niels Justesen</author>
    /// <summary>
    /// The menu item used for showing the highscorelist. The list contains 20 users. 
    /// If the logged in user is not on the highscore list, he will be shown last.
    /// </summary>
    class HighScoreListItem : Selectable {

        static SpriteFont highScoreFont;
        SpriteFont font;

        /// <summary>
        /// The constructor of HighScroeListItem.
        /// </summary>
        public HighScoreListItem(){
            this.font = highScoreFont;
        }

        /// <summary>
        /// Draws the item. If the text is invalid, it turns red.
        /// </summary>
        /// <param name="spriteBatch">The spriebatch used for drawing.</param>
        public override void DrawItem(SpriteBatch spriteBatch){
            int y = (int)Environment.BoxSize.Y+20;
            int x = (int)(Environment.GameAreaSize.X/2-300);
            int divX = 440;
            int width = 600;
            int height = 24;

            spriteBatch.Draw(Textures.Px, new Rectangle(x-12, y - 6, width + 12, height*22 + 12), new Color(0.1f, 0.1f, 0.1f, 0.05f));
            spriteBatch.DrawString(font, "No", new Vector2(x+2, y), Color.White);
            spriteBatch.DrawString(font, "Name", new Vector2(x+48, y), Color.White);
            spriteBatch.DrawString(font, "Score", new Vector2(x + divX, y), Color.White);
            
            Color firstColor = new Color(0.1f, 0.1f, 0.1f, 0.05f);
            Color secondColor = new Color(0.05f, 0.05f, 0.05f, 0.05f);
            Color color = firstColor;
            int num = 1;
            y += height;
            if (Data.UsersOnHighScoreList == null) {
                spriteBatch.DrawString(font, "Cannot connect to database.", new Vector2(x + 110, y+160), Color.Red);
            } else {
                bool userShown = false;
                foreach (user u in Data.UsersOnHighScoreList) {
                    if (Login.UsernameLoggedIn == u.username.ToLowerInvariant()) {
                        spriteBatch.Draw(Textures.Px, new Rectangle(x - 6, y + 1, width + 2, height - 1), Color.YellowGreen);
                        userShown = true;
                    } else {
                        spriteBatch.Draw(Textures.Px, new Rectangle(x - 6, y + 1, width + 2, height - 1), color);
                    }
                    spriteBatch.DrawString(font, u.username.ToUpperInvariant(), new Vector2(x + 6 + 36 + 6, y), Color.White);
                    spriteBatch.DrawString(font, "" + num, new Vector2(x + 6, y), Color.White);
                    spriteBatch.DrawString(font, ""+u.score, new Vector2(x + divX, y), Color.White);
                    if (color == firstColor) {
                        color = secondColor;
                    } else {
                        color = firstColor;
                    }
                    y += height;
                    num++;
                }
                if (!userShown && Login.UsernameLoggedIn != null) {
                    // Get user and rank
                    int rank = Queries.GetRank(Login.UsernameLoggedIn);
                    user u = Queries.GetUser(Login.UsernameLoggedIn);

                    // Draw user
                    spriteBatch.Draw(Textures.Px, new Rectangle(x - 6, y + 1, width + 2, height - 1), Color.YellowGreen);
                    spriteBatch.DrawString(font, u.username, new Vector2(x + 6 + 36 + 6, y), Color.White);
                    spriteBatch.DrawString(font, "" + rank, new Vector2(x + 6, y), Color.White);
                    spriteBatch.DrawString(font, "" + u.score, new Vector2(x + divX, y), Color.White);
                }
            }
        }

        /// <summary>
        /// This item takes no actions.
        /// </summary>
        public override void TakeAction() {
            
        }

        /// <summary>
        /// This item cannot be pressed.
        /// </summary>
        public override void Pressed() {

        }

        /// <summary>
        /// The font used on the highscore list
        /// </summary>
        public static SpriteFont HighScoreFont {
            get { return HighScoreListItem.highScoreFont; }
            set { HighScoreListItem.highScoreFont = value; }
        }
    }
}
