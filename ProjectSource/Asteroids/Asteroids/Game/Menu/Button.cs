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
    /// This class keeps information about a button in the menu. It also draws the button depending on its state, if the button
    /// is chosen or not.
    /// </summary>
    abstract class Button : Selectable {
        Vector2 position;
        Texture2D textureButton;
        Texture2D textureChosen;
       
        /// <summary>
        /// The position of a button.
        /// </summary>
        public Vector2 PositionButton {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// A button that is chosen.
        /// </summary>
        public Texture2D TextureChosen {
            get { return textureChosen; }
            set { textureChosen = value; }
        }

        /// <summary>
        /// A button that is not chosen.
        /// </summary>
        public Texture2D TextureButton {
            get { return textureButton; }
            set { textureButton = value; }
        }

        /// <summary>
        /// A button has two different states. It is either chosen or not chosen. This method decides which button to draw. 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void DrawItem(SpriteBatch spriteBatch) {
            if (Menu.CurrentScreen.CurrentItem.Equals(this)) {
                spriteBatch.Draw(textureChosen, position, Color.White);
            } else {
                spriteBatch.Draw(textureButton, position, Color.White);
            }
        }
    }
}

