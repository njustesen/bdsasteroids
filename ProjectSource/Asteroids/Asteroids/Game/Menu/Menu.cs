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
    /// Enums for navigating to different menu screens.
    /// </summary>
    enum MenuActions {
        Start, Exit, GotoNewGame, GotoSettings, GotoHighscore, GotoAbout,
        GotoControls, GotoSound, GotoVideo, GotoLogin, GotoPlayer1, GotoRegister,
        AddUser, StartPlayer1, StartPlayer2, StartPlayer3, StartPlayer4,
        StartOffline,
        GotoMain
    };

    /// <summary>
    /// This class takes care of making new menuscreens and adding navigation buttons to these menuscreens. It also updates the game which means
    /// that different actions take place while navigating and hits enter on a button. Furthermore a method makes it possible to go back in 
    /// the menu.
    /// </summary>
    static class Menu {
        static MenuScreen currentScreen;
        static bool readyToNavigate = true;
        static bool ingame = false;

        //These fields are used when a navigationButton is placed on the screen.
        static Vector2 firstButton_location = new Vector2((Environment.GameAreaSize.X / 2) - 151, (Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2)) - 165);
        static Vector2 secondButton_location = new Vector2((Environment.GameAreaSize.X / 2) - 151, (Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2)) - 99);
        static Vector2 thirdButton_location = new Vector2((Environment.GameAreaSize.X / 2) - 151, (Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2)) - 33);
        static Vector2 fourthButton_location = new Vector2((Environment.GameAreaSize.X / 2) - 151, (Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2)) + 33);
        static Vector2 fifthButton_location = new Vector2((Environment.GameAreaSize.X / 2) - 151, (Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2)) + 99);
        static Vector2 acceptButton_location = new Vector2((Environment.GameAreaSize.X / 2) - 151, (Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2)) + 140);
        static Vector2 startGameButton_location = new Vector2((Environment.GameAreaSize.X / 2) - 151, (Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2)) + 60);
        
        // These screens are empty menuscreens and is used to add navigation buttons on.
        static MenuScreen mainScreen = new MenuScreen();
        static MenuScreen newgameScreen = new MenuScreen();
        static MenuScreen loginScreen = new MenuScreen();
        static MenuScreen settingsScreen = new MenuScreen();
        static MenuScreen player1Screen = new MenuScreen();
        static MenuScreen registerScreen = new MenuScreen();
        static MenuScreen highscoreScreen = new MenuScreen();
        static MenuScreen aboutScreen = new MenuScreen();
        static MenuScreen controlsScreen = new MenuScreen();
        static MenuScreen soundScreen = new MenuScreen();
        static MenuScreen videoScreen = new MenuScreen();
        static MenuScreen ingameScreen = new MenuScreen();

        /*
        These fields are navigation buttons and they are used as buttons in the menu. They have to different textures depending
        on if a button is chosen or not, a location in the menuscreen and an action which takes you into another menuscreen
        when the button is pressed.
        */
        static NavigateButton exitButton = new NavigateButton(Textures.Exit, Textures.ExitChosen, fifthButton_location, MenuActions.Exit);
        static NavigateButton newGameButton = new NavigateButton(Textures.NewGame, Textures.NewGameChosen, firstButton_location, MenuActions.GotoNewGame);
        static NavigateButton settingsButton = new NavigateButton(Textures.Settings, Textures.SettingsChosen, secondButton_location, MenuActions.GotoSettings);
        static NavigateButton highscoreButton = new NavigateButton(Textures.Highscore, Textures.HighscoreChosen, thirdButton_location, MenuActions.GotoHighscore);
        static NavigateButton aboutButton = new NavigateButton(Textures.About, Textures.AboutChosen, fourthButton_location, MenuActions.GotoAbout);
        static NavigateButton controlsButton = new NavigateButton(Textures.Controls, Textures.ControlsChosen, firstButton_location, MenuActions.GotoControls);
        static NavigateButton soundButton = new NavigateButton(Textures.Sound, Textures.SoundChosen, secondButton_location, MenuActions.GotoSound);
        static NavigateButton videoButton = new NavigateButton(Textures.Video, Textures.VideoChosen, thirdButton_location, MenuActions.GotoVideo);
        static NavigateButton acceptButton = new NavigateButton(Textures.AcceptButton, Textures.AcceptChosen, acceptButton_location, MenuActions.AddUser);
        static NavigateButton player1Button = new NavigateButton(Textures.Player1_Button, Textures.Player1_chosen, firstButton_location, MenuActions.GotoPlayer1);
        static NavigateButton startPlayer1Button = new NavigateButton(Textures.StartGameButton, Textures.StartGameChosen, startGameButton_location, MenuActions.StartPlayer1);
        static NavigateButton player2Button = new NavigateButton(Textures.Player2_Button, Textures.Player2_chosen, secondButton_location, MenuActions.StartPlayer2);
        static NavigateButton player3Button = new NavigateButton(Textures.Player3_Button, Textures.Player3_chosen, thirdButton_location, MenuActions.StartPlayer3);
        static NavigateButton player4Button = new NavigateButton(Textures.Player4_Button, Textures.Player4_chosen, fourthButton_location, MenuActions.StartPlayer4);
        static NavigateButton playButton = new NavigateButton(Textures.Player4_Button, Textures.Player4_chosen, fourthButton_location, MenuActions.Start);
        static NavigateButton quickGameButton = new NavigateButton(Textures.QuickGameButton, Textures.QuickGameChosen, secondButton_location, MenuActions.StartOffline);
        static NavigateButton loginButton = new NavigateButton(Textures.LoginButton, Textures.LoginChosen, thirdButton_location, MenuActions.GotoLogin);
        static NavigateButton registerButton = new NavigateButton(Textures.RegisterButton, Textures.RegisterChosen, fourthButton_location, MenuActions.GotoRegister);

        /// <summary>
        /// The currentscreen in the menu.
        /// </summary>
        public static MenuScreen CurrentScreen {
            get { return Menu.currentScreen; }
            set { Menu.currentScreen = value; }
        }

        /// <summary>
        /// Adding buttons, sets the currentScreen and takes into consideration which music to play in the menu.
        /// </summary>
        public static void setUpMenu() {
            Contract.Ensures(currentScreen == mainScreen);
            currentScreen = mainScreen;
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();
            MediaPlayer.Play(Sounds.MenuMusic);
            AddButtons();
        }

        /// <summary>
        /// Adding buttons to different menu screens and safes it in a list of selectables.
        /// </summary>  
        private static void AddButtons() {
            mainScreen.Selectables = new List<Selectable>(){
               newGameButton,
               settingsButton,
               highscoreButton,
               aboutButton,
               exitButton
            };

            mainScreen.Setup();

            newgameScreen.Selectables = new List<Selectable>() {
               player1Button,
               player2Button,
               player3Button,
               player4Button

            };

            registerScreen.Selectables = new List<Selectable>() {
                new EnterUsernameMenuItem(),
                new EnterPasswordMenuItem(),
                new ReEnterPasswordMenuItem(),
                acceptButton,
            };

            player1Screen.Selectables = new List<Selectable>() {
                quickGameButton,
                loginButton,
                registerButton,

            };

            loginScreen.Selectables = new List<Selectable>() {
                new UsernameMenuItem(),
                new PasswordMenuItem(),
                startPlayer1Button,
            };

            settingsScreen.Selectables = new List<Selectable>(){
               controlsButton,
               soundButton,
               videoButton
            };

            highscoreScreen.Selectables = new List<Selectable>() {
            };

            highscoreScreen.MenuItems = new List<Selectable>() {
                new HighScoreListItem()
            };

            aboutScreen.Selectables = new List<Selectable>() {
            };

            aboutScreen.MenuItems = new List<Selectable>() {
                new About()
            };

            controlsScreen.Selectables = new List<Selectable>() {
                new ControlsMenuItem()
            };

            soundScreen.Selectables = new List<Selectable>() {

            };

            videoScreen.Selectables = new List<Selectable>() {
            };
        }

        /// <summary>
        /// This method sets the controls in the menu.
        /// </summary>
         public static void Update(bool gameStarted) {
             ingame = gameStarted;
             foreach (Selectable s in currentScreen.Selectables) {
                 s.TakeAction();
             }

             if (readyToNavigate) {
                 if (Keyboard.GetState().IsKeyDown(Keys.Enter)) {
                     if (CurrentScreen.CurrentItem != null) {
                         if (ingame) {
                             if (CurrentScreen.CurrentItem == exitButton) {
                                 CurrentScreen.CurrentItem.Pressed();
                             }
                         } else {
                             CurrentScreen.CurrentItem.Pressed();
                         }
                     }
                     readyToNavigate = false;
                 }

                 if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Tab)) {
                    CurrentScreen.Down();
                    readyToNavigate = false;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                    currentScreen.Up();
                    readyToNavigate = false;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.F6)) {
                    GoBack();
                    readyToNavigate = false;
                }
            } else {
                if (!(Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Tab) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Escape)|| Keyboard.GetState().IsKeyDown(Keys.F6))) {
                    readyToNavigate = true;
                }
            }
        }

        /// <summary>
        /// This method handles which screen to enter, depending on which navigate button is pressed.
        /// </summary>
        /// <param name="action"></param>
        public static void HandleAction(MenuActions action) {
            if (action == MenuActions.GotoMain) {
                CurrentScreen = mainScreen;
            } if (action == MenuActions.GotoAbout) {
                CurrentScreen = aboutScreen;
            } else if (action == MenuActions.GotoControls) {
                CurrentScreen = controlsScreen;
            } else if (action == MenuActions.GotoHighscore) {
                CurrentScreen = highscoreScreen;
            } else if (action == MenuActions.GotoNewGame) {
                CurrentScreen = newgameScreen;
            } else if (action == MenuActions.GotoSettings) {
                CurrentScreen = settingsScreen;
            } else if (action == MenuActions.GotoSound) {
                CurrentScreen = soundScreen;
            } else if (action == MenuActions.GotoVideo) {
                CurrentScreen = videoScreen;
            } else if (action == MenuActions.GotoPlayer1) {
                CurrentScreen = player1Screen;
            } else if (action == MenuActions.GotoRegister) {
                CurrentScreen = registerScreen;
            } else if (action == MenuActions.StartPlayer1) {
                try {
                    if (Login.TryLogin()) {
                        currentScreen = mainScreen;
                        Program.StartNewGame(1);
                    }
                } catch (Exception) {}
            } else if (action == MenuActions.StartPlayer2) {
                currentScreen = mainScreen;
                Program.StartNewGame(2);
            } else if (action == MenuActions.StartPlayer3) {
                currentScreen = mainScreen;
                Program.StartNewGame(3);
            } else if (action == MenuActions.StartPlayer4) {
                currentScreen = mainScreen;
                Program.StartNewGame(4);
            } else if (action == MenuActions.Exit) {
                Program.exit();
            } else if (action == MenuActions.GotoLogin) {
                CurrentScreen = loginScreen;
            } else if (action == MenuActions.StartOffline) {
                currentScreen = mainScreen;
                Program.StartNewGame(1);
            } else if (action == MenuActions.AddUser) {
                try {
                    if (Login.PasswordCorrect && !Queries.ExistsUser(Login.NewUsernameToTry)) {
                        Queries.AddUser(Login.NewUsernameToTry, Login.NewPasswordToTry);
                        currentScreen = loginScreen;
                    }
                } catch (Exception) {
                }
            }
            CurrentScreen.Setup();
        }

        /// <summary>
        /// Goes back in the menu depending on the current screen.
        /// </summary>
        public static void GoBack() {
            if (currentScreen == settingsScreen || currentScreen == aboutScreen || currentScreen == newgameScreen || currentScreen == highscoreScreen)  {
                CurrentScreen = mainScreen;
            }
            
            if (currentScreen == player1Screen) {
                CurrentScreen = newgameScreen;
            }

            if (currentScreen == loginScreen || currentScreen == registerScreen ) {
                CurrentScreen = player1Screen;
            }
            
            if (currentScreen == controlsScreen || currentScreen == soundScreen || currentScreen == videoScreen) {
                CurrentScreen = settingsScreen;
            }
        }
    }
}
