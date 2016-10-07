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
    /// A menu item responsible for drawing the about screen.
    /// </summary>
    class About : Selectable {

        static SpriteFont textFont;
        static SpriteFont headLine;
      
        SpriteFont font;
        SpriteFont head;

        /// <summary>
        /// The constructor of About
        /// </summary>
        public About(){
            this.font = textFont;
            this.head = headLine;
        }

        /// <summary>
        /// Draws the item.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used for drawing.</param>
        public override void DrawItem(SpriteBatch spriteBatch){
            int y = (int)Environment.BoxSize.Y+20;
            int x = (int)(Environment.GameAreaSize.X/2-370);
            int width = 900;
            int height = 24;

            spriteBatch.Draw(Textures.Px, new Rectangle(x-115, y+60, width + 70, height*20 -100), new Color(0.1f, 0.1f, 0.1f, 0.05f));
            spriteBatch.DrawString(head, "BDSAsteroids", new Vector2(x+270,y+80),Color.White);
            spriteBatch.DrawString(TextFont, "Authors: Martin kjeldsen, Christian Vestergaard and Niels Justesen", new Vector2(x - 60, y + 130), Color.White);
            spriteBatch.DrawString(TextFont, "This game was created during a project at the IT-University of Copenhagen.", new Vector2(x - 60, y+180), Color.White);
            spriteBatch.DrawString(TextFont, "Contact:", new Vector2(x - 60, y + 280), Color.White);
            spriteBatch.DrawString(TextFont, "Christian Vestergaard: cves@itu.dk", new Vector2(x - 60, y + 300), Color.White);
            spriteBatch.DrawString(TextFont, "Martin Kjeldsen: makj@itu.dk", new Vector2(x - 60, y + 320), Color.White);
            spriteBatch.DrawString(TextFont, "Niels Justesen: noju@itu.dk", new Vector2(x - 60, y + 340), Color.White);

        }

        /// <summary>
        /// This menu item does not take any actions
        /// </summary>
        public override void TakeAction() {
            
        }

        /// <summary>
        /// This item cannot be pressed.
        /// </summary>
        public override void Pressed() {
        }

        /// <summary>
        /// The text font used for this item.
        /// </summary>
        public static SpriteFont TextFont {
            get { return About.textFont; }
            set { About.textFont = value; }
        }

        /// <summary>
        /// The font used for the header in this item.
        /// </summary>
        public static SpriteFont HeadLine {
            get { return About.headLine; }
            set { About.headLine = value; }
        }
    }
 }

