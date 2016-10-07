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
using System.Diagnostics.Contracts;

namespace Asteroids {

    /// <author>Christian Vestergaard, Niels Justesen, Martin Kjeldsen</author>
    /// <summary>
    /// This is the super class for all the objects moving on the screen.
    /// </summary>
    abstract class Movable {
        float speed;
        float rotation = 90;
        float rotationSpeed;
        Vector2 position;
        Vector2 scale;
        Texture2D texture;
        float diameter;
        bool continous = true;

        /// <summary>
        /// Boolean for checking if a movable must go through
        /// walls and come out on the other side or not.
        /// </summary>
        public bool Continous {
            get { return continous; }
            set { continous = value; }
        }

        /// <summary>
        /// Every movable has a diameter so that
        /// we can detect collisions.
        /// </summary>
        public float Diameter {
            get { return diameter; }
            set { diameter = value; }
        }

        /// <summary>
        /// Every movable has a texture.
        /// </summary>
        public Texture2D Texture {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// Every movable moves with some speed.
        /// </summary>
        public float Speed {
            get { return speed; }
            set { speed = value; }
        }

        /// <summary>
        /// The movable moves in with towards where
        /// it is pointing.
        /// </summary>
        public float Rotation {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// The speed of the rotations, used when we
        /// rotate the spaceship.
        /// </summary>
        public float RotationSpeed {
            get { return rotationSpeed; }
            set { rotationSpeed = value; }
        }

        /// <summary>
        /// Every movable has a current position.
        /// </summary>
        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// In order to make the movable fit the screen
        /// we can scale the movable so it is
        /// resolution independent.
        /// </summary>
        public Vector2 Scale {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// In every update, Move(gametime) is called
        /// for every movable.
        /// </summary>
        /// <param name="gameTime">GameTime object containing the time since last update.</param>
        public void Move(GameTime gameTime) {
            Rotate(gameTime);
            float movement = Environment.GlobalScale*speed*(gameTime.ElapsedGameTime.Milliseconds)/100*World.GameSpeed;
            position.X += (float)(movement * Math.Cos(ToRadian(rotation-90)));
            position.Y += (float)(movement * Math.Sin(ToRadian(rotation-90)));
            if (this.continous)
                position = Environment.ConvertPosition(position);
            else if (this.Position.X > Environment.GameAreaSize.X ||
                this.Position.X < 0) {
                World.SpritesToBeRemoved.Add(this);
            }
        }

        /// <summary>
        /// Change the rotation of the current movable
        /// </summary>
        /// <param name="gameTime">GameTime object containing the time since last update.</param>
        public void Rotate(GameTime gameTime) {
            Contract.Ensures(rotation == rotation + rotationSpeed * gameTime.ElapsedGameTime.Milliseconds * World.GameSpeed);
            rotation += rotationSpeed * gameTime.ElapsedGameTime.Milliseconds * World.GameSpeed;
        }

        /// <summary>
        /// Method for converting an angle in degrees to an angle in radians.
        /// </summary>
        /// <param name="ro">Rotation in degrees</param>
        /// <returns>a new float with an angle in degrees converted to an angle in radian</returns>
        public static float ToRadian(float ro) {
            float radian = (float)((ro / 180) * Math.PI);
            return radian;
        }

        /// <summary>
        /// This is a method that will be called on all movables in every update (the Update-method
        /// of the Game1 class). Here it is made abstract in order to make all movables have
        /// their own implementation of TakeActions so that a space ship does not
        /// act like an Asteroid and so on.
        /// <param name="gameTime">GameTime object containing the time since last update.</param>
        public abstract void TakeAction(GameTime gameTime);

        /// <summary>
        /// An invariant making sure our contract is fulfilled.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant() {
            Contract.Invariant(speed >= -0.0001);
        }
    }
}
