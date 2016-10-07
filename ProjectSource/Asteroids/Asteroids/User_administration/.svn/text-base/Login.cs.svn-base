using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids {
    /// <summary>
    /// A class containing information on the user logged in, and the users trying to login.
    /// The EnterPasswordMenuItem and EnterUsernameMenuItem uses this class to hold the information
    /// in the menuItems.
    /// </summary>
    static class Login {
        static String usernameToTry;
        static String passwordToTry;
        static String newUsernameToTry;
        static String newPasswordToTry;
        static String usernameLoggedIn;
        static bool passwordCorrect = false;

        /// <summary>
        /// Try to login with the username and password entered in the menu items.
        /// </summary>
        /// <returns></returns>
        public static bool TryLogin(){
            if (Queries.Login(usernameToTry, passwordToTry)) {
                usernameLoggedIn = usernameToTry;
                return true;
            }
            return false;
        }

        /// <summary>
        /// The password is correct.
        /// </summary>
        public static bool PasswordCorrect {
            get { return Login.passwordCorrect; }
            set { Login.passwordCorrect = value; }
        }

        /// <summary>
        /// The username of the user logged in.
        /// </summary>
        public static String UsernameLoggedIn {
            get { return Login.usernameLoggedIn; }
            set { Login.usernameLoggedIn = value; }
        }

        /// <summary>
        /// The username in the menu item for logging in.
        /// </summary>
        public static String UsernameToTry {
            get { return Login.usernameToTry; }
            set { Login.usernameToTry = value; }
        }

        /// <summary>
        /// The password in the menu item for logging in.
        /// </summary>
        public static String PasswordToTry {
            get { return Login.passwordToTry; }
            set { Login.passwordToTry = value; }
        }

        /// <summary>
        /// The password in the menu item for adding a new user.
        /// </summary>
        public static String NewPasswordToTry {
            get { return Login.newPasswordToTry; }
            set { Login.newPasswordToTry = value; }
        }

        /// <summary>
        /// The username in the menu item for adding a new user.
        /// </summary>
        public static String NewUsernameToTry {
            get { return Login.newUsernameToTry; }
            set { Login.newUsernameToTry = value; }
        }
    }
}
