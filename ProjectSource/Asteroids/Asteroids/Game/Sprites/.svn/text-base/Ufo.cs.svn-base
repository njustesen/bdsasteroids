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

    /// <author>Christian Vestergaard</author>
    /// <summary>
    /// An enum for the direction of the ufo
    /// </summary>
    enum UfoDirection { LEFT, RIGHT }

    /// <author>Christian Vestergaard</author>
    /// <summary>
    /// The movable, Ufo coming after a while when playing a level.
    /// </summary>
    class Ufo : Movable {

        Vector2 oldPosition = new Vector2(0.0f, 0.0f);
        int distanceTraveled = 0;
        UfoDirection direction;
        float straightRotation;
        float shotRotation;
        int shootCount = 0;

        /// <summary>
        /// Direction of the ufo, head left or right.
        /// </summary>
        public UfoDirection Direction {
            get { return direction; }
            set { direction = value; }
        }
        bool isRotated = false;
        private bool isRotatedAgain;

        /// <summary>
        /// How much did the Ufo travel?
        /// </summary>
        public int DistanceTraveled {
            get { return distanceTraveled; }
            set { distanceTraveled = value; }
        }

        /// <summary>
        /// The old position of the ufo
        /// </summary>
        public Vector2 OldPosition {
            get { return oldPosition; }
            set { oldPosition = value; }
        }

        /// <summary>
        /// This is a constructor for the ufo.
        /// </summary>
        /// <param name="po">Position of the ufo</param>
        /// <param name="te">Texture of the ufo</param>
        /// <param name="speed">The speed of the ufo</param>
        /// <param name="rotationSpeed">The rotation speed of the ufo</param>
        /// <param name="rotation">The rotation of the ufo.</param>
        /// <param name="dir">The direction (left or right) of the ufo</param>
        public Ufo(Vector2 po, Texture2D te, float speed, float rotationSpeed, float rotation, UfoDirection dir) {
            Diameter = 36;
            Position = po;
            Texture = te;
            Speed = speed;
            RotationSpeed = rotationSpeed;
            Rotation = rotation;
            Continous = false;
            Scale = new Vector2(0.2f, 0.2f);
            oldPosition = po;
            direction = dir;
            shotRotation = straightRotation;
        }

        /// <summary>
        /// The TakeAction method from Movable. This runs the Course method
        /// making the Ufo run through the screen at some route.
        /// Then it shoots, if it is time to do so.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void TakeAction(GameTime gameTime) {
            Course(gameTime);
            Shoot(gameTime);
        }

        /// <summary>
        /// This method makes the ufo shoot if i modulus 50 is equal to 0. i is summed with 1 at
        /// the end of the method making it a timer for the shoot interval.
        /// </summary>
        /// <param name="gameTime">GameTime object containing the time since last update.</param>
        private void Shoot(GameTime gameTime) {
            Contract.Requires(World.ContainsUfo);
            if (shootCount >= 200) {
                shootCount = 0;
                Sounds.Shoot.Play();
                // Speed is set to 0 because of bug#8
                Bullet bullet = new Bullet(Position, 0, shotRotation, Game1.Ai, Textures.Bullet5);
                World.SpritesToBeAdded.Add(bullet);
                World.BulletsInAir.Add(bullet);
            }
            shootCount += gameTime.ElapsedGameTime.Milliseconds;
            Random r = new Random();
            shotRotation = r.Next(360);
        }

        /// <summary>
        /// This method makes a course for the ufo. It also keeps hold of the distance travelled
        /// in order to change the direction in the right moment.
        /// </summary>
        /// <param name="gameTime">A GameTime object containing the time since last update.</param>
        public void Course(GameTime gameTime) {
            GetTravelledDistance();
            if (distanceTraveled == 0)
                straightRotation = this.Rotation;
            if (!isRotatedAgain)
                MoveIt((1.0f / 5), (2.0f / 5), 45);
            else MoveIt((3.0f / 5), (4.0f / 5), 45);
            this.OldPosition = new Vector2(Position.X, Position.Y);
        }

        /// <summary>
        /// This method changes the direction of the ufo
        /// after a given distance, until an other
        /// given distance.
        /// </summary>
        /// <param name="distanceStart">Change the rotation at this distance</param>
        /// <param name="distanceEnd">Revert the rotation at this distance</param>
        /// <param name="rotationChange">How much is the rotation going to change?</param>
        private void MoveIt(float distanceStart, float distanceEnd, float rotationChange) {
            if (distanceTraveled == 0)
                straightRotation = this.Rotation;
            else if (distanceTraveled > distanceStart * Environment.GameAreaSize.X && distanceTraveled < distanceEnd * Environment.GameAreaSize.X && !isRotated) {
                Rotation = SetRotation(Position, Rotation, direction, rotationChange);
                isRotated = true;
            }
            else if (distanceTraveled > distanceEnd * Environment.GameAreaSize.X && isRotated) {
                Rotation = straightRotation;
                isRotated = false;
                isRotatedAgain = true;
            }
        }

        /// <summary>
        /// A little void method to shorten the MoveIt method.
        /// </summary>
        private void GetTravelledDistance() {
            if (direction == UfoDirection.RIGHT)
                distanceTraveled += (int)Position.X - (int)OldPosition.X;
            else if (direction == UfoDirection.LEFT)
                distanceTraveled += (int)OldPosition.X - (int)Position.X;
        }

        /// <summary>
        /// Sets the rotation of the ufo. It determines where to go
        /// by looking at the current y-position so it never exits
        /// the area of the game.
        /// </summary>
        /// <param name="position">Current position of the ufo</param>
        /// <param name="rotation">Current rotation of the ufo</param>
        /// <param name="direction">Direction of the ufo, left or right.</param>
        /// <param name="rotationChange">How much is the rotation supposed to change?</param>
        /// <returns>Change of rotation either positive or negative
        /// (go upward or downwards)</returns>
        private float SetRotation(Vector2 position, float rotation, UfoDirection direction, float rotationChange) {
            Contract.Requires(Position.Y >= 0 && Position.Y <= Environment.GameAreaSize.Y + Environment.BoxSize.Y);
            if (position.Y > (Environment.GameAreaSize.Y / 2) + Environment.BoxSize.Y)
                if (direction == UfoDirection.LEFT) rotation += rotationChange;
                else rotation -= rotationChange;
            else if (position.Y < (Environment.GameAreaSize.Y / 2) + Environment.BoxSize.Y)
                if (direction == UfoDirection.LEFT) rotation -= rotationChange;
                else rotation += rotationChange;
            return rotation;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant() {
            Contract.Invariant(this.distanceTraveled >= 0);
            Contract.Invariant(this.direction == UfoDirection.LEFT || this.direction == UfoDirection.RIGHT);
        }

    }
}