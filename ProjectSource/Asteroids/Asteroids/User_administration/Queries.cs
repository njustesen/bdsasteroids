using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids {
    /// <author>Niels Justesen</author>
    /// <summary>
    /// A class used for making queries to the database.
    /// </summary>
    static class Queries {
        static bdsasteroidsEntities context = new bdsasteroidsEntities();
        // A weak solution for testing invalid characters. 
        static List<string> invalidChars = new List<string>(){"Å","Æ","Ø"};

        /// <summary>
        /// Loads all users.
        /// </summary>
        /// <returns>All users</returns>
        public static List<user> LoadUsers(){
            var users = from u in context.user
                        select u;
            return users.ToList<user>();
        }

        /// <summary>
        /// Loads a number of users on the highscore list ordered by score.
        /// </summary>
        /// <param name="n"></param>
        /// <returns>The best n users on the highscore list</returns>
        public static List<user> LoadUsersByScore(int n) {
            var users = (from u in context.user
                         orderby u.score descending
                         select u).Take(n);
            return users.ToList<user>() ;
        }

        /// <summary>
        /// Add this score to the highscore list.
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="score">Score</param>
        public static void AddScore(string userName, int score) {
            // get user
            var users = (from u in context.user
                         where u.username==userName
                         select u);

            // add score
            foreach (user u in users){
                if (u.score<score){
                    u.score = score;
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Checks if a user with this name exists.
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <returns>True if true if the user exists.</returns>
        public static bool ExistsUser(string userName) {
            //Check if user exists
            var users = (from u in context.user
                        where u.username == userName
                        select u).Take(1);

            if (users.ToList().Count == 0) {
                return false;
            }               
            return true;
        }

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="userName">The name of the user to add.</param>
        /// <param name="password">The password of the user.</param>
        public static void AddUser(string userName, String password) {
            // Save user if username does not exists
            var users = (from u in context.user
                         where u.username == userName
                         select u).Take(1);

            if (users.ToList().Count == 0) {
                if (CheckChars(userName) && CheckChars(password)) {
                    var user = new user {
                        username = userName,
                        password = password,
                        score = 0
                    };
                    context.user.AddObject(user);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Check if the length of a text is long enough.
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>true if the text is longer than 2 characters.</returns>
        private static bool CheckChars(string text) {
            bool onlyValidChars = true;
            foreach (string s in invalidChars) {
                if (text.Contains(s)) {
                    onlyValidChars = false;
                }
            }
            if (text.Length < 3) {
                onlyValidChars = false;
            }
            return onlyValidChars;
        }

        /// <summary>
        /// Check if a password i correct.
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public static bool Login(string userName, string password) {
            // Find user
            var users = (from u in context.user
                         where u.username == userName
                         select u);

            // Test Password
            if (users.ToList<user>().Count>0) {
                foreach (user u in users) {
                    if (u.password.Equals(password)) {
                        return true;
                    }
                }
            }
            return false;
        }
        
        /// <summary>
        /// </summary>
        /// <param name="userName">The username to find</param>
        /// <returns>Returns a user with the specified username</returns>
        public static user GetUser(string userName) {
            // Find user
            var users = (from u in context.user
                         where u.username == userName
                         select u);
            return users.ToList<user>()[0];
        }

        /// <summary>
        /// </summary>
        /// <param name="userName">The username to find</param>
        /// <returns>The rank of the user</returns>
        public static int GetRank(string userName) {
            var users = from u in context.user
                        orderby u.score descending
                        select u;
            int rank = 0;
            foreach (user u in users) {
                rank++;
                if (u.username == userName) {
                    break;
                }
            }
            return rank;
        }
    }
}
