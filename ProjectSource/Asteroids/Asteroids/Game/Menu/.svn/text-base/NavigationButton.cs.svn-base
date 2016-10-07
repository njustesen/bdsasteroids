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

namespace Asteroids {
    /// <author>Niels Justesen, Martin Kjeldsen</author>
    /// <summary>
    /// A type of button used for navigating to another MenuScreen.
    /// </summary>
    class NavigateButton : Button {

        MenuActions action;

        /// <summary>
        /// The constructor of a Navigate Button.
        /// </summary>
        /// <param name="texture">The texture of the button.</param>
        /// <param name="textureChosen">The texture of the button when selected.</param>
        /// <param name="position">The position of the button.</param>
        /// <param name="action">The action performed when pressed.</param>
        public NavigateButton(Texture2D texture, Texture2D textureChosen, Vector2 position, MenuActions action) {
            TextureButton = texture;
            TextureChosen = textureChosen;
            PositionButton = position;
            this.action = action;
        }
        
        /// <summary>
        /// This method takes action if a button has been entered.
        /// </summary>
        public override void Pressed() {
            Menu.HandleAction(action);
        }

        /// <summary>
        /// This method overrides the abstract method in the class selectable.
        /// </summary>
        public override void TakeAction() {
            
        }
    }
}

