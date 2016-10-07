using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    /// <author>Niels Justesen, Martin Kjeldsen, Christian Vestergaard</author>
    /// <summary>
    /// The different sizes of asteroids.
    /// </summary>
    enum AsteroidSize { Large, Medium, Small };

    /// <author>Niels Justesen</author>
    /// <summary>
    /// A movable asteroid with 3 different sizes. An asteroid always moves with an 
    /// unchanged speed until its destruction.
    /// </summary>
    class Asteroid : Movable {
        private static float minSpeed = 0.2f;
        static List<int> diameters = new List<int>() { 90, 45, 22 };
        AsteroidSize size;
        static List<Vector2> scales = new List<Vector2>() { 
            new Vector2(1.0f,1.0f),
            new Vector2(0.5f,0.5f),
            new Vector2(0.25f,0.25f),
        };

        /// <author>Niels Justesen</author>
        /// <summary>
        /// The constructer of an asteroid.
        /// </summary>
        /// <param name="position">The initial position of the asteroid.</param>
        /// <param name="texture">The texture of the asteroid.</param>
        /// <param name="speed">The speed of the asteroid.</param>
        /// <param name="rotation">The rotation of the asteroid.</param>
        /// <param name="size">The size of the asteroid.</param>
        public Asteroid(Vector2 position, Texture2D texture, float speed, float rotation, AsteroidSize size) {
            Position = position;
            Texture = texture;
            Speed = speed;
            RotationSpeed = 0;
            Rotation = rotation;
            Size = size;
            SetupSize();
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Sets the scale used for drawing and the diameter used for collision checking.
        /// </summary>
        private void SetupSize() {
            if (size == AsteroidSize.Large) {
                Scale = scales[0];
                Diameter = diameters[0];
            } else if (size == AsteroidSize.Medium) {
                Scale = scales[1];
                Diameter = diameters[1];
            } else if (size == AsteroidSize.Small) {
                Scale = scales[2];
                Diameter = diameters[2];
            }
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Asteroids takes no actions, so this implementation is empty.
        /// </summary>
        /// <param name="gameTime">A GameTime containing the time since last update.</param>
        public override void TakeAction(GameTime gameTime) {}

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Takes care of a collision with this asteroid. Unless the asteroid is small, it 
        /// will produce to "childs" and add them to the world.
        /// </summary>
        /// <param name="level">The current level</param>
        public void AsteroidIsHit(Level level) {
            // makes sure that the asteroid has not already hit
            if (!World.SpritesToBeRemoved.Contains(this)){
                // Divide asteroid
                if (Size != AsteroidSize.Small){
                    World.SpritesToBeAdded.Add(World.CreateAsteroidChild(this, level));
                    World.SpritesToBeAdded.Add(World.CreateAsteroidChild(this, level));
                }
                // Remove asteroid
                World.SpritesToBeRemoved.Add(this);
            }
        }

        /// <summary>
        /// The minimum speed asteroids can have. 
        /// </summary>
        public static float MinSpeed {
            get { return Asteroid.minSpeed; }
        }

        /// <summary>
        /// The size of the asteroid.
        /// </summary>
        public AsteroidSize Size {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// An invariant making sure our contract is fulfilled.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant() {
            Contract.Invariant(Speed >= MinSpeed);
        }
    }
}
