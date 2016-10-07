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
    /// Class for the Heads Up Display.
    /// </summary>
    static class HUD {
        static int div = 20;
        static int textIndentation = -124;
        static int boxHeight = 85;
        static int boxWidth = 160;
        static int scoreHeight = 46;
        static int lifeWidth = 26;
        static int shieldBarY = 80;
        static int shieldHeight = 2;
        static int shieldWidth = 162;

        // The anchors are screen coordinates for the positions of the
        // HUD for each player.
        static Vector2 player1Anchor = new Vector2(Div, Environment.BoxSize.Y + Div);
        static Vector2 player2Anchor = new Vector2(Environment.GameAreaSize.X-Div-boxWidth, Environment.BoxSize.Y + Div);
        static Vector2 player3Anchor = new Vector2(Div, Environment.BoxSize.Y + Environment.GameAreaSize.Y - Div - BoxHeight);
        static Vector2 player4Anchor = new Vector2(Environment.GameAreaSize.X-Div-boxWidth, Environment.BoxSize.Y + Environment.GameAreaSize.Y - Div - BoxHeight);

        static List<Vector2> anchors = new List<Vector2>() { player1Anchor, player2Anchor, player3Anchor, player4Anchor };

        static Vector2 scorePos = new Vector2(0.0f, 0.0f);
        static Vector2 lifePos = new Vector2(0.0f, ScoreHeight);

        /// <summary>
        /// Position of the player life
        /// </summary>
        public static Vector2 LifePos {
            get { return HUD.lifePos; }
        }

        /// <summary>
        /// How tall the shield bar is
        /// </summary>
        public static int ShieldHeight {
            get { return HUD.shieldHeight; }
        }

        /// <summary>
        /// How wide the shield bar is
        /// </summary>
        public static int ShieldWidth {
            get { return HUD.shieldWidth; }
        }

        /// <summary>
        /// Position of the player score
        /// </summary>
        public static Vector2 ScorePos {
            get { return HUD.scorePos; }
        }

        /// <summary>
        /// Indentation from screen edges
        /// </summary>
        public static int Div {
            get { return HUD.div; }
        }

        /// <summary>
        /// The height of the HUD box
        /// </summary>
        public static int BoxHeight {
            get { return HUD.boxHeight; }
        }

        /// <summary>
        /// The width of the HUD box
        /// </summary>
        public static int BoxWidth {
            get { return HUD.boxWidth; }
        }

        /// <summary>
        /// Height of the score
        /// </summary>
        public static int ScoreHeight {
            get { return HUD.scoreHeight; }
        }

        /// <summary>
        /// Width of the life "bar"
        /// </summary>
        public static int LifeWidth {
            get { return HUD.lifeWidth; }
        }

        /// <summary>
        /// Indentation of splash text
        /// </summary>
        public static int TextIndentation {
            get { return HUD.textIndentation; }
        }

        /// <summary>
        /// List of HUD anchors
        /// </summary>
        public static List<Vector2> Anchors {
            get { return anchors; }
        }

        /// <summary>
        /// Y-coordinate for Y.
        /// </summary>
        public static int ShieldBarY {
            get { return HUD.shieldBarY; }
        }
    }
}
