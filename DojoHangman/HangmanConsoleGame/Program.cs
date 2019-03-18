using HangmanLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanConsoleGame
{
    class Program
    {
        private static bool _playing = true;
        private static string _playerName = String.Empty;

        static void Main(string[] args)
        {
            _playerName = GetPlayerName();

            while (_playing)
            {
                var currentGame = StartNewGame();

                while (currentGame.WordNotGuessedYet && currentGame.GuessesRemaining > 0)
                {
                    UpdateScreenWithCurrentGuesses(currentGame);

                    MakeAGuess(currentGame);
                }

                UpdateScreenWithCurrentGuesses(currentGame);

                NotifyPlayerOfFinalResults(currentGame);

                _playing = AskIfPlayerWantsToPlayAgain();
            }
        }

        private static HangmanGame StartNewGame()
        {
            var maxGuesses = 10;
            var theWord = "Abracadabra";
            return new HangmanGame(theWord, maxGuesses);

            // HangmanUtilties.GetRandomWord()
            
        }

        private static void UpdateScreenWithCurrentGuesses(HangmanGame currentGame)
        {
           
            Console.Clear();
            Console.WriteLine($"Unguessed Letters: {currentGame.FormattedUnguessedLetters}");
            Console.WriteLine($"Guesses Remaining: {currentGame.GuessesRemaining}");
            Console.WriteLine("");
            Console.WriteLine($"Word: {currentGame.MaskedWord}");
        }

        private static void NotifyPlayerOfFinalResults(HangmanGame currentGame)
        {
            Console.WriteLine($"CONGRATULATIONS, {_playerName}. You guessed it!");
            Console.ReadKey();
        }

        private static bool AskIfPlayerWantsToPlayAgain()
        {
            return true;
        }

        private static void MakeAGuess(HangmanGame currentGame)
        {
            var guess = Console.ReadKey().KeyChar;
            if ( currentGame.HasLetterBeenGuessed(guess) != true)
            {
                currentGame.MakeGuess(guess);
            }
            
        }

        private static string GetPlayerName()
        {
            return Console.ReadLine();
        }
    }
}
