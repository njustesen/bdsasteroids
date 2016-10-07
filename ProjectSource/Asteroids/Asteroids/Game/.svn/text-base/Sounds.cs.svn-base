using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Asteroids{

    /// <author>Christian Vestergaard, Niels Justesen, Martin Kjeldsen</author>
    /// <summary>
    /// Class containing the sounds as well as the music.
    /// </summary>
    static class Sounds{

        static SoundEffect explosion;

        /// <summary>
        /// Sound when an object is exploding.
        /// </summary>
        public static SoundEffect Explosion {
            get { return Sounds.explosion; }
            set { Sounds.explosion = value; }
        }

        static SoundEffect shoot;
        
        /// <summary>
        /// Sound when a movable fires a shot.
        /// </summary>
        public static SoundEffect Shoot
        {
            get { return shoot; }
            set { shoot = value; }
        }

        static SoundEffect shieldHit;

        /// <summary>
        /// When a spaceship with shield on is hit,
        /// this sound is played.
        /// </summary>
        public static SoundEffect ShieldHit {
            get { return Sounds.shieldHit; }
            set { Sounds.shieldHit = value; }
        }

        static SoundEffect spawn;

        /// <summary>
        /// When a spaceship spawns, this sound is played.
        /// </summary>
        public static SoundEffect Spawn {
            get { return Sounds.spawn; }
            set { Sounds.spawn = value; }
        }

        // The ingame music loop.
        static Song loop;

        public static Song Loop{
            get { return loop; }
            set { loop = value; }
        }

        static Song menuMusic;

        /// <summary>
        /// The menu music loop.
        /// </summary>
        public static Song MenuMusic {
            get { return Sounds.menuMusic; }
            set { Sounds.menuMusic = value; }
        }
    }
}