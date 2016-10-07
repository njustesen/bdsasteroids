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
    /// <author>Niels Justesen, Martin Kjeldsen, Christian Vestergaard</author>
    /// <summary>
    /// This class keeps information about a maximum speed, asteroids and asteroid texture at a specific level.
    /// </summary>
    class Level {
        int asteroids;
        float maxSpeed;
        Texture2D asteroidTexture;

        public Level(Texture2D asteroidTexture, int asteroids, float maxSpeed) {
            this.asteroids = asteroids;
            this.maxSpeed = maxSpeed;
            this.asteroidTexture = asteroidTexture;
        }

        /// <summary>
        /// Asteroids in the game at a level.
        /// </summary>
        public int Asteroids {
            get { return asteroids; }
        }

        /// <summary>
        /// The maximum speed in a level.
        /// </summary>
        public float MaxSpeed {
            get { return maxSpeed; }
        }

        /// <summary>
        /// An asteroid texture in a level.
        /// </summary>
        public Texture2D AsteroidTexture {
            get { return asteroidTexture; }
        }
    }
}
