using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics.Contracts;

namespace Asteroids {
    /// <author>Niels Justesen, Martin Kjeldsen, Christian Vestergaard</author>
    /// <summary>
    /// A player has some different properties like a score, a maximum life, a start life, a spawntime, etc. In this class all these properties
    /// are being handled.
    /// </summary>
    class Player {
        static List<Color> colors = new List<Color> { Color.Red, Color.Blue, Color.LimeGreen, Color.Yellow };
        static int maxLife = 6;
        static int startLife = 4;
        static int spawnTime = 15000;
        Color color;
        float shieldEnergy;
        int playerNum;
        static int extraLife = 15000;
        int score;
        int life;
        int timeSinceDeath;
        bool readyToSpawn;
        Boolean alive;

        /// <summary>
        /// The constructor of Player.
        /// </summary>
        /// <param name="number">This players number</param>
        /// <param name="color">This players color</param>
        public Player(int number, Color color) {
            this.color = color;
            playerNum = number;
            life = startLife;
            alive = true;
            shieldEnergy = Shield.MaxShield;
            timeSinceDeath = 0;
            readyToSpawn = true;
        }

        /// <summary>
        /// A score
        /// </summary>
        public int Score {
            get { return score; }
        }

        /// <summary>
        /// This method adds a score and takes into concideration if a player should earn an extra life.
        /// </summary>
        /// <param name="points"></param>
        public void AddScore(int points) {
            Contract.Ensures(Score == Contract.OldValue<int>(Score)+points);
            int xLife = score / extraLife;
            score += points;
            if ((int)(score / extraLife) > xLife && Life!=maxLife){
                life++;
            }
        }

        /// <summary>
        /// A shield energy
        /// </summary>
        public float ShieldEnergy {
            get { return shieldEnergy; }
            set { shieldEnergy = value; }
        }

        /// <summary>
        /// The maximum life for player
        /// </summary>
        public static int MaxLife {
            get { return Player.maxLife; }
        }

        /// <summary>
        /// The startlife for a player
        /// </summary>
        public static int StartLife {
            get { return Player.startLife; }
        }

        /// <summary>
        /// The number of a player
        /// </summary>
        public int PlayerNum {
            get { return playerNum; }
        }

        /// <summary>
        /// Returns a list of colors (red, blue, limegreen and yellow).
        /// </summary>
        public static List<Color> Colors {
            get { return Player.colors; }
        }

        /// <summary>
        /// A color.
        /// </summary>
        public Color Color {
            get { return color; }
        }

        /// <summary>
        /// A life.
        /// </summary>
        public int Life {
            get { return life; }
        }

        /// <summary>
        /// If a player is still alive.
        /// </summary>
        public Boolean Alive {
            get { return alive; }
            set { alive = value; }
        }

        /// <summary>
        /// This method takes care of loosing a life.
        /// </summary>
        public void LooseLife() {
            life --;
        }

        /// <summary>
        /// The time since a player died.
        /// </summary>
        public int TimeSinceDeath {
            get { return timeSinceDeath; }
            set { timeSinceDeath = value; }
        }

        /// <summary>
        /// When a sprite is ready to be spawned.
        /// </summary>
        public bool ReadyToSpawn {
            get { return readyToSpawn; }
            set { readyToSpawn = value; }
        }

        /// <summary>
        /// Updates the time since a player died, and if this value exceeds the spawntime, then the game is ready to spawn.
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateTimeSinceDeath(GameTime gameTime) {
            Contract.Ensures(timeSinceDeath >= 0);
            timeSinceDeath += gameTime.ElapsedGameTime.Milliseconds*World.GameSpeed;
            if (timeSinceDeath >= spawnTime){
                readyToSpawn = true;
                timeSinceDeath = 0;
            }
        }
    }
}
