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

    /// <author>Niels Justesen, Christian Vestergaard</author>
    /// <summary>
    /// In our project a kind of container of all the current
    /// sprites in game. Keeping track of which sprites are
    /// to be removed or added etc.
    /// </summary>
    static class World {
        static Dictionary<AsteroidSize, int> asteroidPoints = new Dictionary<AsteroidSize, int>() { { AsteroidSize.Small, 100 }, { AsteroidSize.Medium, 50 }, { AsteroidSize.Large, 20 } };
        static int ufoScore = 500;
        static double spawnDistance = 170;
        static int randomNumber = -10;
        static int gameSpeed = 8;
        static int numberOfParticles = 80;
        static bool containsUfo = false;
        static int ufoCount;
        static List<Bullet> bulletsInAir = new List<Bullet>();
        static List<Movable> movables = new List<Movable>();
        static List<Asteroid> asteroids = new List<Asteroid>();
        static List<Movable> spritesToBeRemoved = new List<Movable>();
        static List<Movable> spritesToBeAdded = new List<Movable>();
        static List<Spaceship> spaceshipsToBeSpawned = new List<Spaceship>();
        static List<Spaceship> spawnedSpaceships = new List<Spaceship>();
        static List<Ufo> ufo = new List<Ufo>();

        /// <author>Niels Justesen, Christian Vestergaard</author>
        /// <summary>Removes and adds sprites to the world. When objects add 
        /// sprites to the world, they are kept inside spritesToBeRemoved until 
        /// Update is executed.
        /// </summary>
        public static void Update(){
            foreach (Movable m in spritesToBeAdded) {
                if (m is Asteroid){ 
                    asteroids.Add((Asteroid)m);
                }
                if (m is Ufo)
                    ContainsUfo = true;
                movables.Add(m);
            }
            spritesToBeAdded.Clear();

            foreach (Movable m in spritesToBeRemoved){
                movables.Remove(m);
                if (m is Asteroid){ 
                    asteroids.Remove((Asteroid)m);
                }
                if (m is Bullet) {
                    BulletsInAir.Remove((Bullet)m);
                }
                if (m is Ufo)
                    ContainsUfo = false;
            }

            spritesToBeRemoved.Clear();
            foreach (Spaceship s in spawnedSpaceships){
                spaceshipsToBeSpawned.Remove(s);
            }
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Removes everything from the world.
        /// </summary>
        public static void Clear() {
            bulletsInAir.Clear();
            movables.Clear();
            asteroids.Clear();
            spritesToBeRemoved.Clear();
            spritesToBeAdded.Clear();
            spaceshipsToBeSpawned.Clear();
            spawnedSpaceships.Clear();
            ufo.Clear();
            ufoCount = 0;
        }
        

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Creates an asteroid inside the world with random properties defined by the defficulty of the level.
        /// </summary>
        /// <param name="size">The size of the asteroid.</param>
        /// <param name="level">The current level of the game. This specifies the speed of the asteroid.</param>
        /// <returns>An asteroid with random properties.</returns>
        public static Asteroid CreateAsteroid(AsteroidSize size, Level level){
            Contract.Ensures(Contract.Result<Asteroid>().Size == AsteroidSize.Large);
            Contract.Ensures(Contract.Result<Asteroid>().Rotation >= 0);
            Contract.Ensures(Contract.Result<Asteroid>().Rotation <= 360);
            Contract.Ensures(Contract.Result<Asteroid>().Speed >= Asteroid.MinSpeed);
            Contract.Ensures(Contract.Result<Asteroid>().Speed <= level.MaxSpeed);
            Contract.Ensures(Contract.Result<Asteroid>().Position.X >= 0 && Contract.Result<Asteroid>().Position.X <= Environment.GameAreaSize.X);
            Contract.Ensures(Contract.Result<Asteroid>().Position.Y >= 0 && Contract.Result<Asteroid>().Position.Y <= Environment.GameAreaSize.Y + Environment.BoxSize.Y);

            Vector2 position = GetAsteroidLocation();

            float speed = GetAsteroidSpeed(level);
            float rotation = GetAsteroidRotation();
            return new Asteroid(position, level.AsteroidTexture, speed, rotation, size);
        }

        /// <author>Christian Vestergaard</author>
        /// <summary>
        /// Creates an ufo inside the world with a random spawn point at the edge (x = 0 or
        /// x = resolution.end (e.g. 1280)), but with a random Y.
        /// </summary>
        /// <returns>An ufo with a random y-spawn point and an x spawn point at 0 or resolution.end
        /// and speed = 2.</returns>
        public static Ufo CreateUfo() {
            Contract.Requires(!World.ContainsUfo);
            Contract.Ensures(Contract.Result<Ufo>().Direction == UfoDirection.LEFT ||
                Contract.Result<Ufo>().Direction == UfoDirection.RIGHT);

            Vector2 position = GetUfoPosition();
            float rotation;
            UfoDirection direction;
            if (position.X != 0) {
                rotation = -90;
                direction = UfoDirection.LEFT;
            }
            else {
                rotation = 90;
                direction = UfoDirection.RIGHT;
            }

            float speed = 2f;
            float rotationSpeed = 0.0f;

            return new Ufo(position, Textures.Ufo, speed, rotationSpeed, rotation, direction);
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Creates a new asteroid (child) from an exploding asteroid (parent). The child will have a size 
        /// one smaller than the parent, a random rotation and a random speed (but faster than its parent)        
        /// </summary>
        /// <param name="parent">The exploding asteroid.</param>
        /// <param name="level">The current level of the game. This specifies the speed of the asteroid.</param>
        /// <returns>An asteroid with random properties</param>
        /// <returns>An asteroid "child".</returns>
        public static Asteroid CreateAsteroidChild(Asteroid parent, Level level){
            Contract.Requires(parent.Size!=AsteroidSize.Small);
            Contract.Ensures(Contract.Result<Asteroid>().Size == AsteroidSize.Medium || Contract.Result<Asteroid>().Size == AsteroidSize.Small);
            Contract.Ensures(Contract.Result<Asteroid>().Rotation >= 0 && Contract.Result<Asteroid>().Rotation <= 360);
            Contract.Ensures(Contract.Result<Asteroid>().Speed >= parent.Speed);
            Contract.Ensures(Contract.Result<Asteroid>().Position.X >= 0 && Contract.Result<Asteroid>().Position.X <= Environment.GameAreaSize.X);
            Contract.Ensures(Contract.Result<Asteroid>().Position.Y >= 0 && Contract.Result<Asteroid>().Position.Y <= Environment.GameAreaSize.Y + Environment.BoxSize.Y);

            Vector2 position = parent.Position;

            float speed = parent.Speed+GetAsteroidSpeed(level)/3;
            float rotation = GetAsteroidRotation();

            AsteroidSize size = AsteroidSize.Medium;

            if (parent.Size == AsteroidSize.Medium){
                size = AsteroidSize.Small;
            }

            return new Asteroid(position, parent.Texture, speed, rotation, size);
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Generates a random rotation from 0 to 360 degrees. The method creates a new random number generator 
        /// whenever it is called. The method for generating the random number is not only based on the current 
        /// time, since this would make the rotation the same for two asteroids created at the same time. To 
        /// solve this problem it is based on a psoudo-random number generated by the last random number generator. 
        /// </summary>
        /// <returns>A number from 0 to 360.</returns>
        private static float GetAsteroidRotation() {
            Random random = new Random();
            if (randomNumber >= 0) {
                random = new Random(randomNumber);
            }
            randomNumber = random.Next(1000);
            return (float)random.Next(360);
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Generates a random speed for an asteroid. The speed is specified by the difficulty of the current 
        /// level. All asteroids have a minimum speed and a level has a maximum speed. The generated speed will 
        /// be between the to values.
        /// The method for generating the random number is not only based on the current time, since this would 
        /// make the rotation the same for two asteroids created at the same time. To solve this problem it is 
        /// based on a psoudo-random number generated by the last random number generator. 
        /// </summary>
        /// <param name="level">The current level.</param>
        /// <returns>A number between the ateroids minumspeed and the levels maximum speed.</returns>
        private static float GetAsteroidSpeed(Level level) {
            Random random = new Random();
            if (randomNumber >= 0) {
                random = new Random(randomNumber);
            }
            randomNumber = random.Next(1000);
            float speed = 0;
            if (level.MaxSpeed<Asteroid.MinSpeed){
                speed = Asteroid.MinSpeed;
            } else {
                while (speed < Asteroid.MinSpeed){
                    speed = (float)random.Next((int)(level.MaxSpeed*100))/100;
                }
            }
            return speed;
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Generates a random location for an asteroid. The location will be near a corner of the game area.
        /// The method for generating the random number is not only based on the current time, since this would 
        /// make the rotation the same for two asteroids created at the same time. To solve this problem it is 
        /// based on a psoudo-random number generated by the last random number generator. 
        /// </summary>
        /// <returns>A location near a corner of the game area.</returns>
        private static Vector2 GetAsteroidLocation() {
            Random random = new Random();
            if (randomNumber >= 0) {
                random = new Random(randomNumber);
            }
            randomNumber = random.Next(1000);
            Vector2 position = new Vector2(600,500);
            
            int corner = random.Next(4)+1;
            if (corner == 1){
                position = new Vector2(0,Environment.BoxSize.Y);
            } else if (corner == 2){
                position = new Vector2(0, Environment.GameAreaSize.Y+Environment.BoxSize.Y);
            } else if (corner == 3){
                position = new Vector2(Environment.GameAreaSize.X, Environment.GameAreaSize.Y+Environment.BoxSize.Y);
            } else if (corner == 4){
                position = new Vector2(Environment.GameAreaSize.X, Environment.BoxSize.Y);
            }
            
            int offsetX = random.Next(200) - 100;
            int offsetY = random.Next(100) - 50;

            position.X += offsetX;
            position.Y += offsetY;
            
            return position = Environment.ConvertPosition(position);
        }

        /// <author>Christian Vestergaard</author>
        /// <summary>
        /// This method gets the position for the spawn point of the ufo. First it gets a
        /// random number between 0 and 1000, then it finds the module between
        /// the random number and 2 so we get either 1 or 0. If 1 is returned,
        /// the position will be at the right side of the screen, if 0 is returned,
        /// the position will be at x = 0, the left side.
        /// Then it finds a random number between 0 and y, so it will position it self randomly
        /// on the y-axis.
        /// </summary>
        /// <returns>A position for the spawning ufo.</returns>
        private static Vector2 GetUfoPosition() {
            Contract.Ensures(Contract.Result<Vector2>().X >= 0 && Contract.Result<Vector2>().X <= Environment.GameAreaSize.X);
            Contract.Ensures(Contract.Result<Vector2>().Y >= 0 && Contract.Result<Vector2>().Y <= Environment.GameAreaSize.Y + Environment.BoxSize.Y);

            Vector2 position = new Vector2(0.0f,0.0f);

            Random random = new Random();
            int num = random.Next(1000);
            if (num % 2 == 1) {
                position.X = Environment.GameAreaSize.X;
            }
            else position.X = 0;
            position.Y = random.Next((int)Environment.GameAreaSize.Y)+Environment.BoxSize.Y;

            return position;
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Removes an asteroid, creates an explosion and adds points to the responsible player.
        /// </summary>
        public static void DestroyAsteroid(Asteroid asteroid, Level level, Player player) {
            Contract.Ensures(player.Score == Contract.OldValue(player.Score) + AsteroidPoints[asteroid.Size]);
            CreateExplosion(asteroid.Position, asteroid.Texture);
            asteroid.AsteroidIsHit(level);
            player.AddScore(AsteroidPoints[asteroid.Size]);
        }

        /// <author>Niels Justesen</author>
        /// <summary>
        /// Creates a list of particles and plays an explosion sound.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        public static void CreateExplosion(Vector2 position, Texture2D texture){
            for (int i = 0; i < World.NumberOfParticles; i++) {
                World.SpritesToBeAdded.Add(new Particle(position, texture));
            }
            Sounds.Explosion.Play();
        }

        /// <summary>
        /// A list of ufos. Will contain maximum 1 ufo, but
        /// is represented as a list in order to fulfill the
        /// collision algorithm.
        /// </summary>
        public static List<Ufo> Ufo {
            get { return World.ufo; }
            set { World.ufo = value; }
        }

        /// <summary>
        /// A list of spaceships waiting to be spawned.
        /// </summary>
        public static List<Spaceship> SpaceshipsToBeSpawned {
            get { return spaceshipsToBeSpawned; }
        }

        /// <summary>
        /// A list of sprites waiting to be added to the world.
        /// </summary>
        public static List<Movable> SpritesToBeAdded {
            get { return spritesToBeAdded; }
        }

        /// <summary>
        /// A list of sprites waiting to be removed from the world.
        /// </summary>
        public static List<Movable> SpritesToBeRemoved {
            get { return spritesToBeRemoved; }
        }

        /// <summary>
        /// The list of alle movable sprites 
        /// </summary>
        public static List<Movable> Movables {
            get { return movables; }
        }

        /// <summary>
        /// A list of all asteroids in the world.
        /// </summary>
        public static List<Asteroid> Asteroids {
            get { return asteroids; }
        }

        /// <summary>
        /// A list of all just spawned spaceships. These spaceship will be removed from the SpaceshipsToBeSpawned. 
        /// </summary>
        public static List<Spaceship> SpawnedSpaceships  {
            get { return spawnedSpaceships; }
        }

        /// <summary>
        /// The distance of the spawn point that needs to be clear of asteroids.
        /// </summary>
        public static double SpawnDistance {
            get { return spawnDistance; }
        }

        /// <summary>
        /// The gamespeed.
        /// </summary>
        public static int GameSpeed {
            get { return World.gameSpeed; }
            set { World.gameSpeed = value; }
        }

        /// <summary>
        /// The number of particles in an explosion.
        /// </summary>
        public static int NumberOfParticles {
            get { return World.numberOfParticles; }
        }

        /// <summary>
        /// The dictionary containing the points for destroying asteroids depending on the asteroid size.
        /// </summary>
        public static Dictionary<AsteroidSize, int> AsteroidPoints {
            get { return asteroidPoints; }
            set { asteroidPoints = value; }
        }

        /// <summary>
        /// Lists of ufo bullets that have been fired and
        /// have not hit anything yet.
        /// </summary>
        public static List<Bullet> BulletsInAir {
            get { return World.bulletsInAir; }
            set { World.bulletsInAir = value; }
        }

        /// <summary>
        /// Counter for the ufo.
        /// </summary>
        public static int UfoCount {
            get { return World.ufoCount; }
            set { World.ufoCount = value; }
        }

        /// <summary>
        /// Bool, true if the world contains a ufo,
        /// false if it doesn't.
        /// </summary>
        public static bool ContainsUfo {
            get { return World.containsUfo; }
            set { World.containsUfo = value; }
        }

        /// <summary>
        /// How many points does a player get
        /// when he shoots a ufo.
        /// </summary>
        public static int UfoScore {
            get { return World.ufoScore; }
        }
    }
}
