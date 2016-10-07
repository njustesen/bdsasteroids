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
    /// The abstract class for a menu item the user can select.
    /// </summary>
    abstract class Selectable {
        /// <summary>
        /// An abstract method if a button is pressed.
        /// </summary>
        public abstract void Pressed();

        /// <summary>
        /// An abstract method to draw an item.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void DrawItem(SpriteBatch spriteBatch);

        /// <summary>
        /// An abstract method to take action.
        /// </summary>
        public abstract void TakeAction();
    }
}
