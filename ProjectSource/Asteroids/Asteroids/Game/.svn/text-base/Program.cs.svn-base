using System;
using System.Diagnostics.Contracts;

namespace Asteroids{
#if WINDOWS || XBOX
    /// <author>XNA auto-generated code, Niels Justesen, Christian Vestergaard, Martin Kjeldsen</author>
    /// <summary>
    /// The main class used to start new games and terminate the program.
    /// </summary>
    static class Program{

        static Game1 gameRef;
        /// <author>XNA auto-generated code, Niels Justesen, Christian Vestergaard, Martin Kjeldsen</author>
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args){
            using (Game1 game = new Game1()){
                gameRef = game;
                gameRef.Run();
            }
        }

        /// <summary>
        /// Exits the game. This will terminate the program.
        /// </summary>
        public static void exit() {
            gameRef.Exit();
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        /// <param name="players">The number of players in the game.</param>
        public static void StartNewGame(int players) {
            Contract.Requires(players > 0 && players < 5);
            gameRef.Clear();
            gameRef.StartNewGame(players);
        }
    }
#endif
}

