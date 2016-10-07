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
    /// <author>Niels Justesen, Martin Kjeldsen</author>
    /// <summary>
    /// The spaceship class keeps information about a spaceship. Where and how it appears on the screen, how it acts and moves, 
    /// how it shoots, and how the shield level changes.
    /// </summary>
    class Spaceship : Movable {
        Player owner;
        static float shipRotationSpeed = 0.24f;
        static float shipDeacceleration = 0.008f;
        static float maxSpeed = 8;
        static float acceleration = 0.03f;
        static int reloadTime = 0;
        Boolean shieldOn;
        Boolean ableToShoot = true;
        private int timeSinceLastShot = 0;
        bool shieldHit = false;

        /// <summary>
        /// The constructor of a spaceship.
        /// </summary>
        /// <param name="initPosition">The initial position of the spaceship</param>
        /// <param name="texture">The texture of the spaceship.</param>
        /// <param name="owner">The owner of the spaceship.</param>
        public Spaceship(Vector2 initPosition, Texture2D texture, Player owner) {
            Owner = owner;
            Rotation = 0;
            Position = initPosition;
            Texture = texture;
            Scale = new Vector2(0.8f, 0.8f);
            Diameter = 30;
            shieldOn = false;
        }


        /// <summary>
        /// A owner of a spaceship. 
        /// </summary>
        public Player Owner {
            get { return owner; }
            set { owner = value; }
        }

        /// <summary>
        /// The maximum speed of a spaceship.
        /// </summary>
        public float MaxSpeed {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        /// <summary>
        /// Is true if the shield is hit.
        /// </summary>
        public bool ShieldHit {
            get { return shieldHit; }
            set { shieldHit = value; }
        }

        /// <summary>
        /// Shield is on.
        /// </summary>
        public Boolean ShieldOn {
            get { return shieldOn; }
            set { shieldOn = value; }
        }

        /// <summary>
        /// This method takes actions on a spaceship. 
        /// A spaceship has a shieldenergy which is rising with a certain speed until it reaches or exceeds the maximum number 
        /// for a maximumShield. If it does, the player will have maximum shield. 
        /// How a spaceship deaccelerates and sets its speed to zero.
        /// How the values of a spaceships properties is set depending of which actions is performed. 
        /// </summary>
        /// <param name="gameTime">A GameTime containing the time since last update.</param>
        public override void TakeAction(GameTime gameTime) {

            Deaccelerate(gameTime);

            ChangeShieldLevel(gameTime);

            ChangeMovement(gameTime);

            CheckForShooting(gameTime);
            
        }


        /// <summary>
        /// Deaccelerates the spaceship.
        /// </summary>
        /// <param name="gameTime">A GameTime containing the time since last update.</param>
        private void Deaccelerate(GameTime gameTime) {
            if (Speed > 0) {
                if (Speed - shipDeacceleration * gameTime.ElapsedGameTime.Milliseconds >= 0) {
                    Speed -= shipDeacceleration * gameTime.ElapsedGameTime.Milliseconds;
                } else {
                    Speed = 0;
                }
            }
        }

        /// <summary>
        /// Checks if the spaceship should shoot and creates a bullet if it does.
        /// </summary>
        /// <param name="gameTime">A GameTime containing the time since last update.</param>
        private void CheckForShooting(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Triggers.Left > 0) {
                timeSinceLastShot += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastShot > reloadTime && ableToShoot) {
                    ableToShoot = false;
                    timeSinceLastShot = 0;
                    Sounds.Shoot.Play();
                    World.SpritesToBeAdded.Add(new Bullet(Position, Speed, Rotation, owner, Textures.GetPlayerBulletTexture(owner.PlayerNum)));
                }
            } else {
                ableToShoot = true;
            }
        }

        /// <summary>
        /// Changes the speed and rotation of the spaceship.
        /// </summary>
        /// <param name="gameTime">A GameTime containing the time since last update.</param>
        private void ChangeMovement(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Triggers.Right > 0) {
                if (Speed + (acceleration * gameTime.ElapsedGameTime.Milliseconds) * GamePad.GetState(PlayerIndex.One).Triggers.Right * 2 < MaxSpeed) {
                    Speed += (acceleration * gameTime.ElapsedGameTime.Milliseconds) * GamePad.GetState(PlayerIndex.One).Triggers.Right * 2;
                } else {
                    Speed = MaxSpeed;
                }
            }
            // Move right
            Rotation += shipRotationSpeed * gameTime.ElapsedGameTime.Milliseconds * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X) * 1.6f;
        }

        /// <summary>
        /// Changes the energy level of the shield.
        /// </summary>
        /// <param name="gameTime">A GameTime containing the time since last update.</param>
        private void ChangeShieldLevel(GameTime gameTime) {
            // Regenerate shield energy
            if (owner.ShieldEnergy + Shield.ShieldRegen * gameTime.ElapsedGameTime.Milliseconds > Shield.MaxShield) {
                owner.ShieldEnergy = Shield.MaxShield;
            } else {
                owner.ShieldEnergy += Shield.ShieldRegen * gameTime.ElapsedGameTime.Milliseconds;
            }

            // If shield is down, change state and energy level.
            if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed) {
                if (owner.ShieldEnergy > Shield.ShieldUsage * gameTime.ElapsedGameTime.Milliseconds) {
                    shieldOn = true;
                    ShieldHit = false;
                    owner.ShieldEnergy -= Shield.ShieldUsage * gameTime.ElapsedGameTime.Milliseconds;
                } else {
                    shieldOn = false;
                    owner.ShieldEnergy = 0;
                }
            } else {
                shieldOn = false;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant() {
            Contract.Invariant(this.timeSinceLastShot >= 0.0000001);
            Contract.Invariant(Speed >= 0);
        }

    }
}