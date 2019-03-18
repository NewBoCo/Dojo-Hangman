using System;
using System.Linq;
using HangmanLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameVersion
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont titleFont;
        SpriteFont defaultFont;
        HangmanGame currentGame;
        bool playing = false;
       

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            titleFont = Content.Load<SpriteFont>("TitleFont");
            defaultFont = Content.Load<SpriteFont>("MainText");
            this.Window.TextInput += Window_TextInput;
            

            currentGame = new HangmanGame(HangmanUtilities.GetRandomWord(), 7);
            playing = true;

            base.Initialize();
            
        }

        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            if (playing)
            {
                if (!currentGame.HasLetterBeenGuessed(e.Character))
                {
                    currentGame.MakeGuess(e.Character);
                }
            }
            else
            {
                if (Char.ToUpper(e.Character) == 'Y')
                {
                    currentGame = new HangmanGame(HangmanUtilities.GetRandomWord(), 7);
                    playing = true;
                }
            }

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawTitle();
            DrawUnguessedLetters();
            DrawGuessedLetters();
            DrawGuessesRemaining();
            DrawWord();

            DrawHangmanImage();

            if (currentGame.HasWordBeenGuessed)
            {
                DrawCongratulations();
                playing = false;
            }

            if ( currentGame.GuessesRemaining == 0)
            {
                DrawLostMessage();
                playing = false;
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawHangmanImage()
        {
            var index = 7 - currentGame.GuessesRemaining;
            var imageTODraw = $"Hangman{index}";
            var texture = Content.Load<Texture2D>(imageTODraw);
            spriteBatch.Draw(texture, new Vector2(300, 80), Color.White);
        }

        private void DrawLostMessage()
        {
            spriteBatch.DrawString(defaultFont, "I'm sorry, you didn't guess the word in time", new Vector2(2, 150), Color.White);
            spriteBatch.DrawString(defaultFont, "Press 'Y' to play again.", new Vector2(5, 170), Color.White);
        }

        private void DrawCongratulations()
        {
            spriteBatch.DrawString(defaultFont, "CONGRATULATIONS!! You guessed it!", new Vector2(5, 150), Color.White);
            spriteBatch.DrawString(defaultFont, "Press 'Y' to play again.", new Vector2(5, 170), Color.White);
        }

        private void DrawWord()
        {
            var word = $"Word: {currentGame.MaskedWord}";
            spriteBatch.DrawString(defaultFont, word, new Vector2(5, 130), Color.White);
        }

        private void DrawUnguessedLetters()
        {
            var unselectedLetters = $"Unguessed Letters: {currentGame.FormattedUnguessedLetters}";
            spriteBatch.DrawString(defaultFont, unselectedLetters, new Vector2(5, 50), Color.White);
        }

        private void DrawGuessedLetters()
        {
            var text = $"Guessed Letters: {currentGame.FormattedGuessedLetters}";
            spriteBatch.DrawString(defaultFont, text, new Vector2(5, 70), Color.White);
        }

        private void DrawGuessesRemaining()
        {
            var unselectedLetters = $"Guesses Remaining: {currentGame.GuessesRemaining}";
            spriteBatch.DrawString(defaultFont, unselectedLetters, new Vector2(5, 90), Color.White);
        }

        private void DrawTitle()
        {
            var textSize = titleFont.MeasureString("Welcome to Hangman");
            var width = GraphicsDevice.Viewport.Width;
            spriteBatch.DrawString(titleFont, "Welcome to Hangman", new Vector2(width/2 - textSize.X/2, 0), Color.White);
        }
    }
}
