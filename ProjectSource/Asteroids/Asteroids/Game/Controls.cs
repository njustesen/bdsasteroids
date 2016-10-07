using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Contracts;

namespace Asteroids {

    /// <author>Christian Vestergaard, Niels Justesen</author>
    /// <summary>
    /// Just an enumeration type for the actions of the player
    /// </summary>
    enum Actions { Accelerate, Left, Right, Shoot, Shield }

    /// <author>Christian Vestergaard</author>
    /// <summary>
    /// Class for the player controls.
    /// </summary>
    static class Controls {

        /// <summary>
        /// Here we assign the keys for player 1.
        /// </summary>
        static Dictionary<Actions, Keys> player1 = new Dictionary<Actions,Keys>() {
            {Actions.Accelerate,Keys.Up}, 
            {Actions.Left,Keys.Left}, 
            {Actions.Right,Keys.Right}, 
            {Actions.Shoot,Keys.RightShift},
            {Actions.Shield,Keys.RightAlt}
        };

        /// <summary>
        /// Here we assign the keys for player 2.
        /// </summary>
        static Dictionary<Actions, Keys> player2 = new Dictionary<Actions,Keys>() {
            {Actions.Accelerate,Keys.W}, 
            {Actions.Left,Keys.A}, 
            {Actions.Right,Keys.D}, 
            {Actions.Shoot,Keys.Q},
            {Actions.Shield,Keys.D1}
        };

        /// <summary>
        /// Here we assign the keys for player 3.
        /// </summary>
        static Dictionary<Actions, Keys> player3 = new Dictionary<Actions,Keys>() {
            {Actions.Accelerate,Keys.I}, 
            {Actions.Left,Keys.J}, 
            {Actions.Right,Keys.L}, 
            {Actions.Shoot,Keys.Space},
            {Actions.Shield,Keys.V}
        };

        /// <summary>
        /// Here we assign the keys for player 1.
        /// </summary>
        static Dictionary<Actions, Keys> player4 = new Dictionary<Actions,Keys>() {
            {Actions.Accelerate,Keys.NumPad8}, 
            {Actions.Left,Keys.NumPad4}, 
            {Actions.Right,Keys.NumPad6}, 
            {Actions.Shoot,Keys.NumPad9},
            {Actions.Shield,Keys.NumPad3}
        };

        /// <author>Christian Vestergaard</author>
        /// <summary>
        /// Returns the key pressed by the current player acting
        /// as he does.
        /// </summary>
        /// <param name="playerNumber">The current player</param>
        /// <param name="action">The action of the current player</param>
        /// <returns>Key for the action of this player</returns>
        public static Keys GetKey(int playerNumber, Actions action) {
            Contract.Requires(playerNumber > 0 && playerNumber < 5);
            if (playerNumber == 1) {
                return player1[action];
            }
            else if (playerNumber == 2) {
                return player2[action];
            }
            else if (playerNumber == 3) {
                return player3[action];
            }
            else {
                return player4[action];
            }
        }
        
        public static Dictionary<Actions, Keys> Player1 {
            get { return Controls.player1; }
            set { Controls.player1 = value; }
        }

        public static Dictionary<Actions, Keys> Player2 {
            get { return Controls.player2; }
            set { Controls.player2 = value; }
        }

        public static Dictionary<Actions, Keys> Player3 {
            get { return Controls.player3; }
            set { Controls.player3 = value; }
        }

        public static Dictionary<Actions, Keys> Player4 {
            get { return Controls.player4; }
            set { Controls.player4 = value; }
        }
    }
}
