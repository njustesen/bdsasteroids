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
    /// <author>Niels Justesen, Christian Vestergaard, Martin Kjeldsen</author>
    /// <summary>
    /// A bullet moving with in an unchanged direction until its range is reached.
    /// The bullet has an owner used for collision checking.
    /// </summary>
    class Bullet : Movable{
        // This is the amount of milliseconds*gamespeed bullets travel
        static float bulletTravelTime = 3200;
        static float bulletSpeed = 8;
        float timeTraveled = 0;
        Player owner;

        /// <author>Niels Justesen</author>
        /// <summary>
        /// The constructor of a bullet.
        /// </summary>
        /// <param name="position">The initial position of a bullet. This should be set as the position of the shooting object.</param>
        /// <param name="speed">The speed of the bullet. This is the speed of the shooting object. The static bullet speed 
        /// will be added.</param>
        /// <param name="rotation">The rotation of the bullet. This should be set to the roation of the object.</param>
        /// <param name="owner">The object firing this bullet.</param>
        /// <param name="texture">The texture of the bullet.</param>
        public Bullet(Vector2 position, float speed, float rotation, Player owner, Texture2D texture) {
            Speed = speed+bulletSpeed;
            Position = position;
            Rotation = rotation;
            Texture = texture;
            Scale = new Vector2(0.8f, 0.8f);
            Owner = owner;
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Checks if the range is reached. If it is, the bullet will be removed from the world.
        /// </summary>
        /// <param name="gameTime">A GameTime containing the time since last update.</param>
        public override void TakeAction(GameTime gameTime){
            timeTraveled += gameTime.ElapsedGameTime.Milliseconds*World.GameSpeed;
            if (timeTraveled > bulletTravelTime) {
                World.SpritesToBeRemoved.Add(this);
            }
        }

        /// <summary>
        /// The owner of this bullet.
        /// </summary>
        public Player Owner {
            get { return owner; }
            set { owner = value; }
        }
    }
}
