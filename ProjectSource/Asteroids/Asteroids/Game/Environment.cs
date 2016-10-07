
using System;
using System.Diagnostics.Contracts;
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
    /// <author>Niels Justesen, Christian Vestergaard, Martin Kjeldsen</author>
    /// <summary>
    /// This class describes the monitor related environment including the screen 
    /// resolution, screen ratio and the size of the game area.
    /// The game area can have two different size, 1280x720 or 1024x576. The ratio 
    /// is locked to 16:9. 
    /// </summary>
    static class Environment {
        static Vector2 screenSize = new Vector2(0.0f, 0.0f);
        static Vector2 gameAreaSize;
        static Vector2 gameAreaSize1280 = new Vector2(1280f, 720f);
        static Vector2 gameAreaSize1024 = new Vector2(1024f, 576f);
        static Vector2 boxSize = new Vector2(0.0f, 0.0f);
        static float globalScale = 1.0f;
        static float globalScale1280 = 1.0f;
        static float globalScale1024 = 0.8f;

        /// <summary>
        /// Converts positions that are not in the game area to valid positions inside.
        /// The purpose of this method is to make the world unescapeable, as new positions 
        /// needs to be converted to a valid position.
        /// </summary>
        /// <param name="pos">The new position</param>
        /// <returns>A valid position inside the game area corresponding to the input position.</returns>
        public static Vector2 ConvertPosition (Vector2 pos){
            Contract.Requires(pos.X >= GameAreaSize.X * -1 &&
                              pos.X <= GameAreaSize.X * 2 &&
                              pos.Y <= GameAreaSize.Y * 2 &&
                              pos.Y >= GameAreaSize.Y * -1);
            Vector2 position = pos;
            if (pos.X > gameAreaSize.X) {
                position.X -= gameAreaSize.X;
            }
            if (pos.X < 0) {
                position.X += gameAreaSize.X;
            }
            if (pos.Y < boxSize.Y) {
                position.Y += gameAreaSize.Y;
            }
            if (pos.Y > gameAreaSize.Y+boxSize.Y) {
                position.Y -= gameAreaSize.Y;
            }
            return position;
        }

        /// <summary>
        /// The scale of all objects. 
        /// </summary>
        public static float GlobalScale {
            get { return Environment.globalScale; }
            set { Environment.globalScale = value; }
        }

        /// <summary>
        /// The scale of all sprites in an 1280x720 environment.
        /// </summary>
        public static float GlobalScale1280 {
            get { return Environment.globalScale1280; }
            set { Environment.globalScale1280 = value; }
        }

        /// <summary>
        /// The scale of all sprites in an 1024x576 environment.
        /// </summary>
        public static float GlobalScale1024 {
            get { return Environment.globalScale1024; }
            set { Environment.globalScale1024 = value; }
        }

        /// <summary>
        /// The size of the screen resolution.
        /// </summary>
        public static Vector2 ScreenSize {
            get { return screenSize; }
            set { screenSize = value; }
        }

        /// <summary>
        /// The size of the game area size.
        /// </summary>
        public static Vector2 GameAreaSize {
            get { return gameAreaSize; }
            set {
                gameAreaSize = value;
            }
        }

        /// <summary>
        /// The size of the horizontal black boxes.
        /// </summary>
        public static Vector2 BoxSize {
            get { return boxSize; }
            set { boxSize = value; }
        }

        /// <summary>
        /// The size of the game area size in an 1280x720 environment.
        /// </summary>
        public static Vector2 GameAreaSize1280 {
            get { return gameAreaSize1280; }
        }

        /// <summary>
        /// The size of an 1024x576 environment.
        /// </summary>
        public static Vector2 GameAreaSize1024 {
            get { return gameAreaSize1024; }
        }
    }
}