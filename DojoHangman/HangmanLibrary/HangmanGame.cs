using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanLibrary
{
    public class HangmanGame
    {
        readonly SortedSet<char> _guessedLetters = new SortedSet<char>();
        readonly string _word = String.Empty;

        public HangmanGame(string word, int maxGuesses = 10)
        {
            _word = word.ToUpper();
            _guessedLetters = new SortedSet<char>();
            MaxGuesses = maxGuesses;
        }

        public int GuessCount
        {
            get { return _guessedLetters.Count; }
        }

        public int IncorrectGuessCount
        {
            get
            {
                return _guessedLetters.Except(_word.ToCharArray()).Count();
            }
        }

        public int GuessesRemaining
        {
            get
            {
                return MaxGuesses - IncorrectGuessCount;
            }
        }

        public bool HasLetterBeenGuessed(char letter)
        {
            return _guessedLetters.Contains(Char.ToUpper(letter));
        }

        public void MakeGuess(char guess)
        {
            if ( GuessesRemaining == 0)
            {
                throw new InvalidOperationException("You have no guesses left");
            }

            if (HasWordBeenGuessed)
            {
                throw new InvalidOperationException("The word has already been guessed");
            }

            if (HasLetterBeenGuessed(guess))
            {
                throw new InvalidOperationException($"You have already guessed {guess.ToString()}");
            }

            _guessedLetters.Add(Char.ToUpper(guess));
        }

        public bool HasWordBeenGuessed
        {
            get
            {
                var lettersInWord = new SortedSet<char>(_word);
                return lettersInWord.SetEquals(lettersInWord.Intersect(_guessedLetters));
            }
        }

        public bool WordNotGuessedYet
        {
            get
            {
                return !HasWordBeenGuessed;
            }
        }

        public IEnumerable<char> UnguessedLetters
        {
            get
            {
                return HangmanUtilities.AllLetters.Except(_guessedLetters);
            }
        }

        public IEnumerable<char> GuessedLetters
        {
            get
            {
                return _guessedLetters;
            }
        }

        public string MaskedWord
        {
            get
            {
                var maskedCharacters = _word
                    .ToCharArray()
                    .SelectMany(
                        letter => GetMaskedLetter(letter));

                return new string(maskedCharacters.ToArray()).Trim();
            }
        }

        public int MaxGuesses { get; }

        public string FormattedUnguessedLetters
        {
            get
            {
                return String.Join(" ", UnguessedLetters);
            }

        }

        public string FormattedGuessedLetters
        {
            get
            {
                return String.Join(" ", GuessedLetters);
            }
        }

        private IEnumerable<char> GetMaskedLetter(char letter)
        {
            return HasLetterBeenGuessed(letter) ?
                                new char[] { letter, ' ' } :
                                new char[] { '_', ' ' };
        }
    }
}
