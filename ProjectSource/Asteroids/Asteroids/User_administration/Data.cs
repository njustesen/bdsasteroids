using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids {
    /// <author>Niels Justesen</author>
    /// <summary>
    /// A class containing data from the highscore list.
    /// </summary>
    static class Data {

        static List<user> usersOnHighScoreList;

        /// <summary>
        /// The list of users on the highscore list.
        /// </summary>
        public static List<user> UsersOnHighScoreList {
            get { return Data.usersOnHighScoreList; }
            set { Data.usersOnHighScoreList = value; }
        }
        
        /// <summary>
        /// Download the highscorelist with 20 users.
        /// </summary>
        public static void LoadData() {
            try {
                usersOnHighScoreList = Queries.LoadUsersByScore(20);
            } catch (Exception) {

            }
        }
    }
}
