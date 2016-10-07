using System;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroids {
    /// <author>XNA atuo-generated code, Niels Justesen, Christian Vestergaard, Martin Kjeldsen</author>
    /// <summary>
    /// This class controls the flow of the game. To start the game, the run method is called.
    /// It tries to update the game for 60 times per second, and will also draw sprites to the screen.
    /// Game1 uses the World class as a collection of sprites and the Environment class as a description 
    /// of the screen size and settings.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        Boolean pause;
        Boolean pauseButtonReleased;

        Boolean menu;
        Boolean menuButtonReleased;

        Boolean restartButtonReleased;
        Boolean soundButtonReleased;

        Boolean endGameButtonReleased;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<DisplayMode> AllowedDisplayModes = new List<DisplayMode>();

        SpriteFont scoreFont;
        SpriteFont levelFont;

        static Player ai;
        int ufoTime;
        bool ufoThisLevel;
        bool firstLevelHasLoaded = false;

        List<Level> levels;
        Level currentLevel;
        int levelNum;
        int timeBetweenLevels;
        int timeSinceCompletion;

        bool musicOn;
        bool gameStarted;

        public bool GameStarted {
            get { return gameStarted; }
            set { gameStarted = value; }
        }
        bool gameOver;

        List<Player> players;

        static Vector2 singlePlayerSpawnPoint;

        static List<Vector2> spawnPoints;

        /// <author>XNA auto-generated code.</author>
        /// <summary>
        /// The constructor of Game1
        /// </summary>
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <author>XNA auto-generated code, Niels Justesen, Christian Vestergaard, Martin Kjeldsen</author>
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            Contract.Requires(GameStarted != true);
            Contract.Ensures(Environment.GameAreaSize == Environment.GameAreaSize1024 || 
                             Environment.GameAreaSize == Environment.GameAreaSize1280);
            Contract.Ensures(levels.Count>0);
            Contract.Ensures(Environment.ScreenSize.X == 1024 ||
                             Environment.ScreenSize.X == 1280);
            Contract.Ensures(gameStarted != true);
            Contract.Ensures(spawnPoints.Count == 4);

            pause = false;
            pauseButtonReleased = true;
            menu = true;
            menuButtonReleased = true;
            restartButtonReleased = true;
            soundButtonReleased = true;
            endGameButtonReleased = true;
            gameStarted = false;
            gameOver = false;
            musicOn = true;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // The initialization order
            SetupTextures();
            SetupFonts();
            SetupEnvironment();
            SetupSounds();
            Menu.setUpMenu();
            Data.LoadData();
            SetupSpawnPoints();
            SetupLevels();
            SetupScreen();
            players = new List<Player>();

            base.Initialize();
        }

        /// <summary>
        /// Starts a new game. The music will restart, unless the user has turned it off.
        /// </summary>
        /// <param name="players">The number of players in the game.</param>
        public void StartNewGame(int numOfPlayers) {
            Contract.Requires(GameStarted == false);
            Contract.Ensures(players.Count > 0);
            menu = false;
            pause = false;
            gameStarted = true;
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();
            MediaPlayer.Volume = 0.5f;
            if (musicOn){
                MediaPlayer.Play(Sounds.Loop);
            }
            SetupPlayers(numOfPlayers);
            AddSpaceShips();
        }

        /// <summary>
        /// Sets the spawn points for each player.
        /// </summary>
        private void SetupSpawnPoints() {
            int div = 250;
            singlePlayerSpawnPoint =
                new Vector2(Environment.GameAreaSize.X / 2, Environment.BoxSize.Y + (Environment.GameAreaSize.Y / 2));
            spawnPoints = new List<Vector2>(){
                new Vector2(div, Environment.BoxSize.Y+div),
                new Vector2(Environment.GameAreaSize.X-div, Environment.BoxSize.Y+div),
                new Vector2(div, Environment.BoxSize.Y+Environment.GameAreaSize.Y-div),
                new Vector2(Environment.GameAreaSize.X-div, Environment.BoxSize.Y+Environment.GameAreaSize.Y - div)
            };
        }

        /// <summary>
        /// Sets up the levels, and sets the current level number to 0 because the has not yet started.
        /// </summary>
        private void SetupLevels() {
            Contract.Requires(!gameStarted);
            Contract.Ensures(levels.Count > 0);
            Contract.Ensures(levelNum == 0);

            timeBetweenLevels = 20000;
            timeSinceCompletion = 0;
            levelNum = 0;
            levels = new List<Level> { 
                // first level cycle
                new Level(Textures.Asteroid1, 6, 1.6f),
                new Level(Textures.Asteroid2, 7, 1.8f),
                new Level(Textures.Asteroid3, 8, 2f),
                new Level(Textures.Asteroid4, 9, 2.2f),
                new Level(Textures.Asteroid5, 10, 2.4f),
                new Level(Textures.Asteroid6, 11, 2.55f),
                new Level(Textures.Asteroid7, 12, 2.7f),
                new Level(Textures.Asteroid8, 13, 2.85f),
                new Level(Textures.Asteroid9, 14, 3.0f),
                new Level(Textures.Asteroid10, 15, 3.2f),
                
                // second level cycle
                new Level(Textures.Asteroid1, 7, 3.3f),
                new Level(Textures.Asteroid2, 8, 3.3f),
                new Level(Textures.Asteroid3, 9, 3.4f),
                new Level(Textures.Asteroid4, 10, 3.4f),
                new Level(Textures.Asteroid5, 11, 3.5f),
                new Level(Textures.Asteroid6, 12, 3.5f),
                new Level(Textures.Asteroid7, 13, 3.6f),
                new Level(Textures.Asteroid8, 14, 3.6f),
                new Level(Textures.Asteroid9, 15, 3.7f),
                new Level(Textures.Asteroid10, 16, 3.7f),

                // third level cycle
                new Level(Textures.Asteroid1, 8, 3.8f),
                new Level(Textures.Asteroid2, 9, 3.8f),
                new Level(Textures.Asteroid3, 10, 3.9f),
                new Level(Textures.Asteroid4, 11, 3.9f),
                new Level(Textures.Asteroid5, 12, 4.0f),
                new Level(Textures.Asteroid6, 13, 4.1f),
                new Level(Textures.Asteroid7, 14, 4.2f),
                new Level(Textures.Asteroid8, 15, 4.4f),
                new Level(Textures.Asteroid9, 16, 4.7f),
                new Level(Textures.Asteroid10, 17, 5.2f),
                
                // Impossible levels created, so the game is unable to complete.
                new Level(Textures.Asteroid9, 20, 5.5f),

                new Level(Textures.Asteroid8, 25, 6.0f),
                
                new Level(Textures.Asteroid7, 30, 7.0f),

                new Level(Textures.Asteroid6, 35, 8.0f),
                // Last level. Completely impossible to complete.
                new Level(Textures.Asteroid5, 50, 10.0f),
            };
        }

        /// <summary>
        /// Loads all textures and sets up the Textures class.
        /// </summary>
        private void SetupTextures() {
            Contract.Requires(!gameStarted);
            Textures.Bullet1 = Content.Load<Texture2D>(@"Bullet/bullet1");
            Textures.Bullet2 = Content.Load<Texture2D>(@"Bullet/bullet2");
            Textures.Bullet3 = Content.Load<Texture2D>(@"Bullet/bullet3");
            Textures.Bullet4 = Content.Load<Texture2D>(@"Bullet/bullet4");
            Textures.Bullet5 = Content.Load<Texture2D>(@"Bullet/bullet5");
            Textures.Ship1 = Content.Load<Texture2D>(@"Spaceship/fly1");
            Textures.Ship2 = Content.Load<Texture2D>(@"Spaceship/fly2");
            Textures.Ship3 = Content.Load<Texture2D>(@"Spaceship/fly3");
            Textures.Ship4 = Content.Load<Texture2D>(@"Spaceship/fly4");
            Textures.Life1 = Content.Load<Texture2D>(@"Life/life1");
            Textures.Life2 = Content.Load<Texture2D>(@"Life/life2");
            Textures.Life3 = Content.Load<Texture2D>(@"Life/life3");
            Textures.Life4 = Content.Load<Texture2D>(@"Life/life4");
            Textures.Ufo = Content.Load<Texture2D>(@"Ufo/UFO");
            Textures.Shield = Content.Load<Texture2D>(@"Spaceship/skjold");
            Textures.Shield_hit = Content.Load<Texture2D>(@"Spaceship/skjold_hit");
            
            Textures.Background1 = Content.Load<Texture2D>(@"Background/moon1280");
            Textures.Background2 = Content.Load<Texture2D>(@"Background/earth1280");
            Textures.Background3 = Content.Load<Texture2D>(@"Background/venus1280");
            Textures.Background = Textures.Background1;

            Textures.ParticleMetal = Content.Load<Texture2D>(@"particleMetal_grande");
            Textures.Logo = Content.Load<Texture2D>(@"Logo/logoTiny");
            Textures.Asteroid1 = Content.Load<Texture2D>(@"Asteroids/Asteroid1");
            Textures.Asteroid2 = Content.Load<Texture2D>(@"Asteroids/Asteroid2");
            Textures.Asteroid3 = Content.Load<Texture2D>(@"Asteroids/Asteroid3");
            Textures.Asteroid4 = Content.Load<Texture2D>(@"Asteroids/Asteroid4");
            Textures.Asteroid5 = Content.Load<Texture2D>(@"Asteroids/Asteroid5");
            Textures.Asteroid6 = Content.Load<Texture2D>(@"Asteroids/Asteroid6");
            Textures.Asteroid7 = Content.Load<Texture2D>(@"Asteroids/Asteroid7");
            Textures.Asteroid8 = Content.Load<Texture2D>(@"Asteroids/Asteroid8");
            Textures.Asteroid9 = Content.Load<Texture2D>(@"Asteroids/Asteroid9");
            Textures.Asteroid10 = Content.Load<Texture2D>(@"Asteroids/Asteroid10");
            Textures.NewGame = Content.Load<Texture2D>(@"Menu/Ikke_valgt/NewGame_Button");
            Textures.Settings = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Settings_Button");
            Textures.Highscore = Content.Load<Texture2D>(@"Menu/Ikke_valgt/HighScore_Button");
            Textures.About = Content.Load<Texture2D>(@"Menu/Ikke_valgt/About_Button");
            Textures.Exit = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Exit_Button");
            Textures.ExitChosen = Content.Load<Texture2D>(@"Menu/Valgt/Exit_Button");
            Textures.AcceptButton = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Accept_Button");
            Textures.AcceptChosen = Content.Load<Texture2D>(@"Menu/Valgt/Accept_Button");
            Textures.Player1_Button = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Player1");
            Textures.Player1_chosen = Content.Load<Texture2D>(@"Menu/Valgt/Player1");
            Textures.Player2_Button = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Player2");
            Textures.Player2_chosen = Content.Load<Texture2D>(@"Menu/Valgt/Player2");
            Textures.Player3_Button = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Player3");
            Textures.Player3_chosen = Content.Load<Texture2D>(@"Menu/Valgt/Player3");
            Textures.Player4_Button = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Player4");
            Textures.Player4_chosen = Content.Load<Texture2D>(@"Menu/Valgt/Player4");
            Textures.QuickGameButton = Content.Load<Texture2D>(@"Menu/Ikke_valgt/QuickGame_Button");
            Textures.QuickGameChosen = Content.Load<Texture2D>(@"Menu/Valgt/QuickGame_Button");
            Textures.LoginButton = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Login_Button");
            Textures.LoginChosen = Content.Load<Texture2D>(@"Menu/Valgt/Login_Button");
            Textures.RegisterButton = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Register_Button");
            Textures.RegisterChosen = Content.Load<Texture2D>(@"Menu/Valgt/Register_Button");
            Textures.StartGameButton = Content.Load<Texture2D>(@"Menu/Ikke_valgt/StartGame_Button");
            Textures.StartGameChosen = Content.Load<Texture2D>(@"Menu/Valgt/StartGame_Button");
            Textures.TextField = Content.Load<Texture2D>(@"Menu/TextFields/TextField");
            Textures.TextFieldChosen = Content.Load<Texture2D>(@"Menu/TextFields/TextField_valgt");
            Textures.Controls = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Controls_Button");
            Textures.Sound = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Sounds_Button");
            Textures.Video = Content.Load<Texture2D>(@"Menu/Ikke_valgt/Video_Button");
            Textures.SettingsChosen = Content.Load<Texture2D>(@"Menu/Valgt/Settings_Button");
            Textures.HighscoreChosen = Content.Load<Texture2D>(@"Menu/Valgt/Highscore_Button");
            Textures.NewGameChosen = Content.Load<Texture2D>(@"Menu/Valgt/NewGame_Button");
            Textures.SoundChosen = Content.Load<Texture2D>(@"Menu/Valgt/Sounds_Button");
            Textures.ControlsChosen = Content.Load<Texture2D>(@"Menu/Valgt/Controls_Button");
            Textures.VideoChosen = Content.Load<Texture2D>(@"Menu/Valgt/Video_Button");
            Textures.AboutChosen = Content.Load<Texture2D>(@"Menu/Valgt/About_Button");
        }

        /// <summary>
        /// Sets up the fonts used in the game.
        /// </summary>
        private void SetupFonts() {
            Contract.Requires(!gameStarted);
            Textures.Px = Content.Load<Texture2D>(@"Line/px");
            scoreFont = Content.Load<SpriteFont>(@"Fonts/Georgia_26");
            levelFont = Content.Load<SpriteFont>(@"Fonts/Georgia_100");
            SpriteFont Georgia_12 = Content.Load<SpriteFont>(@"Fonts/Georgia_12");
            SpriteFont Georgia_14 = Content.Load<SpriteFont>(@"Fonts/Georgia_14");
            About.HeadLine = Georgia_14;
            About.TextFont = Georgia_12;
            HighScoreListItem.HighScoreFont = Georgia_14;
            UsernameMenuItem.Font = Georgia_14;
            PasswordMenuItem.Font = Georgia_14;
            EnterUsernameMenuItem.Font = Georgia_14;
            EnterPasswordMenuItem.Font = Georgia_14;
            ReEnterPasswordMenuItem.Font = Georgia_14;
            ControlsMenuItem.Font = Georgia_14;
        }

        /// <summary>
        /// Loads the sounds and sets up the Sounds class.
        /// </summary>
        private void SetupSounds() {
            Contract.Requires(!gameStarted);
            MediaPlayer.IsRepeating = true;
            Sounds.Shoot = Content.Load<SoundEffect>(@"Sounds/skyd");
            Sounds.ShieldHit = Content.Load<SoundEffect>(@"Sounds/shieldHit");
            Sounds.Explosion = Content.Load<SoundEffect>(@"Sounds/explosion");
            Sounds.Spawn = Content.Load<SoundEffect>(@"Sounds/spawn");

            Sounds.Loop = Content.Load<Song>(@"Sounds/loop");
            Sounds.MenuMusic = Content.Load<Song>(@"Sounds/asteroids");
        }

        /// <summary>
        /// Adds spaceships to the world.
        /// </summary>
        private void AddSpaceShips() {
            Contract.Requires(players.Count > 0 && players.Count < 5);
            foreach (Player p in players) {
                Vector2 position;
                if (players.Count == 1) {
                    position = singlePlayerSpawnPoint;
                }
                else {
                    position = spawnPoints[p.PlayerNum - 1];
                }
                Texture2D ship = Textures.GetPlayerTexture(p.PlayerNum);
                World.SpritesToBeAdded.Add(new Spaceship(position, ship, p));
                p.ReadyToSpawn = false;
            }
        }

        /// <summary>
        /// Creates and adds players to the game. An AI player will also be created to be the owner of the spaceship.
        /// </summary>
        /// <param name="num">The number of players to be added to the game.</param>
        private void SetupPlayers(int num) {
            Contract.Requires(num > 0);
            Contract.Ensures(players.Count > 0);
            for(int i = 0; i<num; i++){
                players.Add(new Player(i+1, Player.Colors[i]));
            }
            ai = new Player(5, Color.Purple);
        }

        /// <summary>
        /// Loads a specified level by adding its amount of asteroids and starting the ufo counter.
        /// </summary>
        /// <param name="level">The level to load.</param>
        private void LoadLevel(Level level) {
            Contract.Requires(levels.Contains(level));
            Contract.Requires(level.Asteroids > 0);
            Contract.Requires(level.MaxSpeed > 0);

            // change background at certain levels
            if (levelNum == 10){
                Textures.Background = Textures.Background2;
            }
            if (levelNum == 20){
                Textures.Background = Textures.Background3;
            }

            firstLevelHasLoaded = true;
            currentLevel = level;
            Random random = new Random();
            ufoThisLevel = false;
            ufoTime = random.Next(2000)*10+10000*10;
            World.UfoCount = 0;
            World.Ufo.Add(World.CreateUfo());
            for (int i = 0; i < level.Asteroids; i++) {
                World.SpritesToBeAdded.Add(World.CreateAsteroid(AsteroidSize.Large, currentLevel));
            }
        }

        /// <summary>
        /// Sets the screen resolution and the fullscreen view
        /// </summary>
        private void SetupScreen() {
            Contract.Requires(Environment.ScreenSize.X == 1024 || Environment.ScreenSize.X == 1280);
            graphics.PreferredBackBufferWidth = (int)Environment.ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)Environment.ScreenSize.Y;
            graphics.ApplyChanges();
            graphics.ToggleFullScreen();
        }

        /// <summary>
        /// Sets up the environment class, and finds a fitting screen resolution.
        /// </summary>
        private void SetupEnvironment() {
            Contract.Ensures((Environment.ScreenSize.X == 1024 &&
                             Environment.GameAreaSize.X == 1024 &&
                             Environment.GlobalScale == Environment.GlobalScale1024)
                             ||
                             (Environment.ScreenSize.X == 1280 &&
                             Environment.GameAreaSize.X == 1280 &&
                             Environment.GlobalScale == Environment.GlobalScale1280));

            float initAspectRatio = GraphicsDevice.DisplayMode.AspectRatio;

            // Finds the legal display modes
            foreach (DisplayMode displayMode
                in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes) {
                if (displayMode.Width == Environment.GameAreaSize1024.X || displayMode.Width == Environment.GameAreaSize1280.X){
                    AllowedDisplayModes.Add(displayMode);
                }
            }

            // Finds the display mode with best ratio
            float bestRatioDif = 1.0f;
            DisplayMode bestDisplayMode = AllowedDisplayModes[0];
            foreach (DisplayMode displayMode in AllowedDisplayModes) {
                float ratioDif = (float)Math.Sqrt((displayMode.AspectRatio - initAspectRatio) * (displayMode.AspectRatio - initAspectRatio));
                if (ratioDif == bestRatioDif && displayMode.Width>bestDisplayMode.Width) {
                    bestDisplayMode = displayMode;
                }
                else if (ratioDif < bestRatioDif + 0.02f || ratioDif < bestRatioDif - 0.02f) {
                    bestDisplayMode = displayMode;
                    bestRatioDif = ratioDif;
                }
            }

            // Setup Environment
            Environment.ScreenSize = new Vector2(bestDisplayMode.Width, bestDisplayMode.Height);

            if (Environment.ScreenSize.X == 1024) {
                Environment.GameAreaSize = Environment.GameAreaSize1024;
                Environment.GlobalScale = Environment.GlobalScale1024;
            } else if (Environment.ScreenSize.X == 1280) {
                Environment.GameAreaSize = Environment.GameAreaSize1280;
                Environment.GlobalScale = Environment.GlobalScale1280;
            } else {
                Exit();
            }

            Environment.BoxSize = new Vector2(Environment.GameAreaSize.X, (Environment.ScreenSize.Y - Environment.GameAreaSize.Y) / 2);
        }

        /// <author>XNA Auto-generated code.</author>
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
        }


        /// <author>XNA Auto-generated code.</author>
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }


        /// <author>XNA Auto-generated code, Niels Justesen, Christian Vestergaard, Martin Kjeldsen</author>
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // if the menu is shown, update it.
            if (menu) {
                Menu.Update(gameStarted);
            }

            // if the game is active update it.
            if (!pause && !menu) {
                // Check if the level is completed.
                if (LevelIsCompleted()) {
                    CheckLoadTime(gameTime);
                }
                // add an UFO if its time
                if (World.UfoCount >= ufoTime && !ufoThisLevel && firstLevelHasLoaded) {
                    ufoThisLevel = true;
                    World.Ufo.Clear();
                    World.Ufo.Add(World.CreateUfo());
                    World.SpritesToBeAdded.Add(World.Ufo[0]);
                }
                // Add time to UfoCount
                if (gameStarted){ 
                    World.UfoCount += gameTime.ElapsedGameTime.Milliseconds*World.GameSpeed;
                }

                // Add and remove object to the world
                World.Update();

                // Check collisions and make sprites act
                foreach (Movable m in World.Movables) {
                    if (m is Spaceship) {
                        CheckCollision((Spaceship)m);
                    }
                    if (m is Bullet){
                        CheckCollision((Bullet)m);
                    }
                    m.TakeAction(gameTime);
                    m.Move(gameTime);
                }
                SpawnSpaceShips(gameTime);
            }

            // Check for non-game-related user input
            UpdateInput();

            if (!gameOver && gameStarted) {
                CheckIfGameOver();
            }

            base.Update(gameTime);

            if (World.Ufo.Count == 1 && !World.ContainsUfo)
                World.Ufo.Clear();
        }

        /// <summary>
        /// Checks for game over.
        /// </summary>
        private void CheckIfGameOver() {
            Contract.Requires(gameStarted);
            if (World.Movables.Count != 0) {
                Boolean go = true;
                // check if all players are dead
                foreach (Player p in players) {
                    if (p.Alive) {
                        go = false;
                    }
                }
                gameOver = go;
                // if game is over, add score to highscore list.
                if (gameOver) {
                    gameStarted = false;
                    AddScore();
                    menu = true;
                    try {
                        Data.UsersOnHighScoreList = Queries.LoadUsersByScore(20);
                    } catch (Exception) {
                    }
                    // Goto highscore list in menu
                    Menu.HandleAction(MenuActions.GotoHighscore);
                    World.Update();
                }
            }
        }

        /// <summary>
        /// If the user is logged in, add the score to the highscore list.
        /// </summary>
        private void AddScore() {
            Contract.Requires(gameOver);
            if (Login.UsernameLoggedIn != null){
                Queries.AddScore(Login.UsernameLoggedIn, players[0].Score);
            }
        }

        /// <summary>
        /// Loads next level if current level is completed and waiting time between levels is over.
        /// </summary>
        /// <param name="gameTime">The GameTime containing the the since last update.</param>
        private void CheckLoadTime(GameTime gameTime) {
            if (timeSinceCompletion < timeBetweenLevels) {
                timeSinceCompletion += gameTime.ElapsedGameTime.Milliseconds*World.GameSpeed;
            }
            else {
                timeSinceCompletion = 0;
                levelNum++;
                LoadLevel(levels[levelNum - 1]);
            }
        }

        /// <summary>
        /// Spawns spaceships that are ready to spawn if spawn area is clear or the user presses the spawn button combo.
        /// </summary>
        /// <param name="gameTime">The GameTime containg the time since last update.</param>
        private void SpawnSpaceShips(GameTime gameTime) {
            foreach (Spaceship s in World.SpaceshipsToBeSpawned) {
                if (s.Owner.Life > 0) {
                    if (s.Owner.ReadyToSpawn) {
                        Vector2 spawnPoint;
                        if (players.Count > 1) {
                            spawnPoint = spawnPoints[s.Owner.PlayerNum - 1];
                        } else {
                            spawnPoint = singlePlayerSpawnPoint;
                        }
                        if (GetShortestDistance(spawnPoint) > World.SpawnDistance * Environment.GlobalScale || (Keyboard.GetState().IsKeyDown(Controls.GetKey(s.Owner.PlayerNum, Actions.Shield))) && Keyboard.GetState().IsKeyDown(Controls.GetKey(s.Owner.PlayerNum, Actions.Shoot))) {
                            Texture2D ship = Textures.GetPlayerTexture(s.Owner.PlayerNum);
                            World.Movables.Add(new Spaceship(spawnPoint, ship, s.Owner));
                            World.SpawnedSpaceships.Add(s);
                            s.Owner.LooseLife();
                            s.Owner.ReadyToSpawn = false;
                            Sounds.Spawn.Play();
                        }
                    } else {
                        s.Owner.UpdateTimeSinceDeath(gameTime);
                    }
                } else {
                    s.Owner.Alive = false;
                }
            }
        }

        /// <summary>
        /// Returns the shortest distance to an object in the world. The method does not 
        /// meassure beyond the game area. This method will return 1000, if no object within 
        /// that distance is found.
        /// </summary>
        /// <param name="spawnPoint">The point to meassure from.</param>
        /// <returns>The distance to the closest object.</returns>
        private double GetShortestDistance(Vector2 spawnPoint) {
            Contract.Ensures(Contract.Result<double>() <= 1000 &&
                             Contract.Result<double>() >= 0);
            double shortestDistance = 1000;
            foreach (Asteroid a in World.Asteroids) {
                double distance = Math.Sqrt(Math.Pow((spawnPoint.X - (a.Position.X)), 2) + Math.Pow((spawnPoint.Y - a.Position.Y), 2));
                if (distance < shortestDistance) {
                    shortestDistance = distance;
                }
            }
            if (World.ContainsUfo) {
                foreach (Ufo u in World.Ufo) {
                    double distance = Math.Sqrt(Math.Pow((spawnPoint.X - (u.Position.X)), 2) + Math.Pow((spawnPoint.Y - u.Position.Y), 2));
                    if (distance < shortestDistance) {
                        shortestDistance = distance;
                    }
                }
            }
            return shortestDistance;
        }

        /// <summary>
        /// Checks if a bullets is hitting something.
        /// </summary>
        /// <param name="bullet">The Bullet to test for collision.</param>
        private void CheckCollision(Bullet bullet) {
            // check for asteroid collisions
            foreach (Asteroid a in World.Asteroids) {
                if (!World.SpritesToBeRemoved.Contains(a)) {

                    Vector2 astPos = new Vector2(a.Position.X, a.Position.Y);

                    double distance = Math.Sqrt(Math.Pow((bullet.Position.X - (astPos.X)), 2) + Math.Pow((bullet.Position.Y - astPos.Y), 2));
                    if (distance < a.Diameter / 2 * Environment.GlobalScale) {
                        World.SpritesToBeRemoved.Add(bullet);
                        World.DestroyAsteroid(a, currentLevel, bullet.Owner);
                    }
                }
            }
            // check for spaceship bullet and UFO collision
            if (World.ContainsUfo) {
                foreach (Ufo u in World.Ufo) {
                    if (!World.SpritesToBeRemoved.Contains(u)) {

                        Vector2 ufoPos = new Vector2(u.Position.X, u.Position.Y);

                        double distance = Math.Sqrt(Math.Pow((bullet.Position.X - (ufoPos.X)), 2) + Math.Pow((bullet.Position.Y - ufoPos.Y), 2));
                        if (distance < u.Diameter / 2 * Environment.GlobalScale && bullet.Owner != Ai) {
                            World.CreateExplosion(u.Position, Textures.ParticleMetal);
                            bullet.Owner.AddScore(World.UfoScore);
                            World.SpritesToBeRemoved.Add(bullet);
                            World.SpritesToBeRemoved.Add(u);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a spaceships hits somethihg.
        /// </summary>
        /// <param name="spacesip">Spaceship</param>
        private void CheckCollision(Spaceship spaceship) {
            // check for asteroid collision
            foreach (Asteroid a in World.Asteroids) {
                if (!World.SpritesToBeRemoved.Contains(a)) {
                    Vector2 astPos = new Vector2(a.Position.X, a.Position.Y);

                    double distance = Math.Sqrt(Math.Pow((spaceship.Position.X - (astPos.X)), 2) + Math.Pow((spaceship.Position.Y - astPos.Y), 2));
                    if ((distance - spaceship.Diameter / 2 * Environment.GlobalScale) < a.Diameter / 2 * Environment.GlobalScale) {
                        if (spaceship.ShieldOn) {
                            World.DestroyAsteroid(a, currentLevel, spaceship.Owner);
                            spaceship.Owner.ShieldEnergy -= Shield.HitEnergy;
                            if (!spaceship.ShieldHit) spaceship.ShieldHit = true;
                            Sounds.ShieldHit.Play();
                        }
                        else {
                            World.DestroyAsteroid(a, currentLevel, spaceship.Owner);
                            World.CreateExplosion(spaceship.Position, Textures.ParticleMetal);
                            World.SpritesToBeRemoved.Add(spaceship);
                            World.SpaceshipsToBeSpawned.Add(spaceship);
                        }
                    }
                }
            }
            // check for UFO-bullet collision
            foreach (Bullet b in World.BulletsInAir) {
                if (!World.SpritesToBeRemoved.Contains(b)) {
                    Vector2 bulletPos = new Vector2(b.Position.X, b.Position.Y);
                    double distance = Math.Sqrt(Math.Pow((spaceship.Position.X - (bulletPos.X)), 2) + Math.Pow((spaceship.Position.Y - bulletPos.Y), 2));
                    if ((distance - spaceship.Diameter / 2 * Environment.GlobalScale) < b.Diameter / 2 * Environment.GlobalScale) {
                        if (spaceship.ShieldOn) {
                            spaceship.Owner.ShieldEnergy -= Shield.HitEnergy;
                            World.SpritesToBeRemoved.Add(b);
                        } else {
                            World.SpritesToBeRemoved.Add(spaceship);
                            World.SpritesToBeRemoved.Add(b);
                            World.SpaceshipsToBeSpawned.Add(spaceship);
                            World.CreateExplosion(spaceship.Position, Textures.ParticleMetal);
                        }
                    }
                }
            }
            // check for ufo-collision
            if (World.ContainsUfo) {
                foreach (Ufo u in World.Ufo) {
                    if (!World.SpritesToBeRemoved.Contains(u)) {
                        Vector2 ufoPos = new Vector2(u.Position.X, u.Position.Y);
                        double distance = Math.Sqrt(Math.Pow((spaceship.Position.X - (ufoPos.X)), 2) + Math.Pow((spaceship.Position.Y - ufoPos.Y), 2));
                        if ((distance - spaceship.Diameter / 2 * Environment.GlobalScale) < u.Diameter / 2 * Environment.GlobalScale) {
                            if (spaceship.ShieldOn) {
                                spaceship.Owner.ShieldEnergy -= Shield.HitEnergy;
                                World.CreateExplosion(u.Position, Textures.ParticleMetal);
                                World.SpritesToBeRemoved.Add(u);
                                if (!spaceship.ShieldHit) spaceship.ShieldHit = true;
                                Sounds.ShieldHit.Play();
                            } else {
                                World.CreateExplosion(spaceship.Position, Textures.ParticleMetal);
                                World.CreateExplosion(u.Position, Textures.ParticleMetal);
                                World.SpritesToBeRemoved.Add(spaceship);
                                World.SpritesToBeRemoved.Add(u);
                                World.SpaceshipsToBeSpawned.Add(spaceship);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the level is completed.
        /// </summary>
        /// <returns>True if the number of asteroids in the world is zero.</returns>
        private bool LevelIsCompleted() {
            Contract.Requires(gameStarted && !gameOver);
            if (World.Asteroids.Count == 0) {
                return true;
            }
            return false;
        }

        /// <author>XNA auto-generated code, Niels Justesen, Christian Vestergaard, Martin Kjeldsen</author>
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            spriteBatch.Begin();

            // Draw background image
            DrawBackground();

            // Draw bullets and asteroids
            DrawSprites();

            // Draw spaceships
            DrawSpaceShips();

            // Draw horizontal boxes
            DrawHorisontalBoxes();

            // Draw HUD
            DrawHUD();

            // Draw next level text?
            if (timeSinceCompletion != 0 && !menu) {
                DrawLevelText();
            }

            if (menu || pause) {
                spriteBatch.Draw(Textures.Background, new Rectangle(0, 0, (int)Environment.ScreenSize.X, (int)Environment.ScreenSize.Y), new Color(0.0f, 0.0f, 0.0f, 0.5f));
            }
            if (menu) {
                DrawMenu();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws the level text.
        /// </summary>
        private void DrawLevelText() {
            spriteBatch.DrawString(levelFont, "Level " + (levelNum + 1), new Vector2((Environment.GameAreaSize.X / 2 - 300), (Environment.GameAreaSize.Y / 2 -100)), Color.Red);
        }

        /// <summary>
        /// Draws the menu.
        /// </summary>
        private void DrawMenu() {
            spriteBatch.Draw(Textures.Logo, new Vector2(14, Environment.BoxSize.Y + Environment.GameAreaSize.Y - Textures.Logo.Height - 14), Color.White);
            foreach (Selectable s in Menu.CurrentScreen.Selectables) {
                s.DrawItem(spriteBatch);
            }

            foreach (Selectable s in Menu.CurrentScreen.MenuItems) {
                s.DrawItem(spriteBatch);
            }
        }

        /// <summary>
        /// Draws the Heads Up Display
        /// </summary>
        private void DrawHUD() {
            // Draw score
            foreach (Player p in players) {
                Vector2 anchor = HUD.Anchors[p.PlayerNum - 1];
                for (int i = 0; i < Player.MaxLife; i++) {
                    spriteBatch.DrawString(scoreFont, Convert.ToString(p.Score), anchor + HUD.ScorePos + new Vector2(HUD.TextIndentation, 0.0f), Player.Colors[p.PlayerNum - 1], 0.0f, new Vector2(-125.0f, 0.0f), 1.0f, SpriteEffects.None, 1.0f);
                }

                // Draw shield bar
                if (p.Alive) {
                    float shieldPercentage = p.ShieldEnergy / Shield.MaxShield;
                    spriteBatch.Draw(Textures.Px, new Rectangle((int)anchor.X - 1, (int)anchor.Y - 1 + HUD.ShieldBarY, HUD.ShieldWidth + 2, HUD.ShieldHeight + 2), Color.White);
                    spriteBatch.Draw(Textures.Px, new Rectangle((int)anchor.X, (int)anchor.Y + HUD.ShieldBarY, (int)(shieldPercentage * HUD.ShieldWidth), HUD.ShieldHeight), p.Color);
                }
            }

            // Draw life
            foreach (Player p in players) {
                Vector2 anchor = HUD.Anchors[p.PlayerNum - 1];
                for (int i = 0; i < p.Life; i++) {
                    Vector2 lifeDiv = new Vector2((HUD.LifeWidth * i), 0.0f);
                    spriteBatch.Draw(Textures.GetPlayerLifeTexture(p.PlayerNum), anchor + HUD.LifePos + lifeDiv, Color.White);
                }
            }
        }

        /// <summary>
        /// Draws the horisontal boxes.
        /// </summary>
        private void DrawHorisontalBoxes() {
            spriteBatch.Draw(Textures.Px, new Rectangle(0, 0, (int)Environment.GameAreaSize.X, (int)Environment.BoxSize.Y), Color.Black);
            spriteBatch.Draw(Textures.Px, new Rectangle(0, (int)(Environment.GameAreaSize.Y + (int)Environment.BoxSize.Y), (int)Environment.ScreenSize.X, (int)Environment.ScreenSize.Y), Color.Black);
        }

        /// <summary>
        /// Draws the sprites.
        /// </summary>
        private void DrawSprites() {
            foreach (Movable m in World.Movables) {
                if (!(m is Spaceship) && !World.SpritesToBeRemoved.Contains(m)) {
                    float rotationInRadians = (float)((m.Rotation) * (Math.PI / 180));
                    spriteBatch.Draw(m.Texture, m.Position, null, Color.White, rotationInRadians, new Vector2(m.Texture.Width / 2, m.Texture.Height / 2), m.Scale * Environment.GlobalScale, SpriteEffects.None, 0f);
                }
            }
        }

        /// <summary>
        /// Draws the spaceships.
        /// </summary>
        private void DrawSpaceShips() {
            foreach (Movable m in World.Movables) {
                if (m is Spaceship && !World.SpritesToBeRemoved.Contains(m)) {
                    float rotationInRadians = (float)((m.Rotation) * (Math.PI / 180));
                    spriteBatch.Draw(m.Texture, m.Position, null, Color.White, rotationInRadians, new Vector2(m.Texture.Width / 2, m.Texture.Height / 2), m.Scale * Environment.GlobalScale, SpriteEffects.None, 0f);
                    if (((Spaceship)m).ShieldOn)
                        if (((Spaceship)m).ShieldHit) spriteBatch.Draw(Textures.Shield_hit, m.Position, null, Color.White, rotationInRadians, new Vector2(m.Texture.Width / 2, m.Texture.Height / 2), m.Scale * Environment.GlobalScale, SpriteEffects.None, 0f);
                        else spriteBatch.Draw(Textures.Shield, m.Position, null, Color.White, rotationInRadians, new Vector2(m.Texture.Width / 2, m.Texture.Height / 2), m.Scale * Environment.GlobalScale, SpriteEffects.None, 0f);
                }
            }
        }

        /// <summary>
        /// Draws the background.
        /// </summary>
        private void DrawBackground() {
            spriteBatch.Draw(Textures.Background, new Rectangle(0, (int)Environment.BoxSize.Y, (int)Environment.GameAreaSize.X, (int)Environment.GameAreaSize.Y), Color.White);
        }

        /// <summary>
        /// Checks for non-game-related user input.
        /// </summary>
        private void UpdateInput() {
            MenuCheck();
            PauseCheck();
            CommandCheck();
        }

        /// <summary>
        /// Checks for non-game-related user input.
        /// </summary>
        private void CommandCheck() {
            // Is the sound key down?
            if (Keyboard.GetState().IsKeyDown(Keys.F10) && soundButtonReleased) {
                soundButtonReleased = false;
                if (MediaPlayer.State == MediaState.Playing){
                    MediaPlayer.Pause();
                    musicOn = false;
                } else {
                    MediaPlayer.Resume();
                    musicOn = true;
                }
            }
            // Is the sound key up?
            if (Keyboard.GetState().IsKeyUp(Keys.F10)) {
                soundButtonReleased = true;
            }

            // Is the restart key up?
            if (Keyboard.GetState().IsKeyUp(Keys.F2)) {
                restartButtonReleased = true;
            }

            // Is the Restart key down?
            if (Keyboard.GetState().IsKeyDown(Keys.F2) && restartButtonReleased) {
                restartButtonReleased = false;
                int num = players.Count();
                Clear();
                Program.StartNewGame(num);
            }

            // Is the restart key up?
            if (Keyboard.GetState().IsKeyUp(Keys.F1)) {
                endGameButtonReleased = true;
            }

            // Is the Restart key down?
            if (Keyboard.GetState().IsKeyDown(Keys.F1) && endGameButtonReleased) {
                gameOver = true;
                gameStarted = false;
                menu = true;
                Menu.HandleAction(MenuActions.GotoMain);
                World.Update();
            }
        }

        /// <summary>
        /// Checks for non-game-related user input.
        /// </summary>
        private void MenuCheck() {
            // Is the Escape key down?
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.F6)) && menuButtonReleased) {
                if (!menu) {
                    menu = true;
                } else if (gameStarted) {
                    menu = false;
                    // Set menu as default
                }
                menuButtonReleased = false;
            }

            // Is the Escape key down?
            if (Keyboard.GetState().IsKeyUp(Keys.Escape) && Keyboard.GetState().IsKeyUp(Keys.F6)) {
                menuButtonReleased = true;
            }
        }

        /// <summary>
        /// Checks for non-game-related user input.
        /// </summary>
        private void PauseCheck() {
            // Is the P keys down?
            if (Keyboard.GetState().IsKeyDown(Keys.P)) {
                if (pauseButtonReleased) {
                    if (!pause) {
                        pause = true;
                    } else {
                        pause = false;
                    }
                    pauseButtonReleased = false;
                }
            }

            // Is the P key up?
            if (Keyboard.GetState().IsKeyUp(Keys.P)) {
                pauseButtonReleased = true;
            }
        }

        /// <summary>
        /// Clears the game data, so its ready for a new game.
        /// </summary>
        public void Clear() {
            players = new List<Player>();
            World.Clear();
            gameOver = false;
            gameStarted = false;
            levelNum = 0;
        }

        /// <summary>
        /// The current level.
        /// </summary>
        internal Level CurrentLevel {
            get { return currentLevel; }
        }

        /// <summary>
        /// The AI Player.
        /// </summary>
        internal static Player Ai {
            get { return Game1.ai; }
            set { Game1.ai = value; }
        }
    }
}












