using System;
using System.Collections.Generic;
using FluentAssertions;
using HangmanLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DojoHangmanTests
{
    [TestClass]
    public class HangmanGameTests
    {
        [TestMethod]
        public void ctor_CreationOfGame_InitialValuesAreSet()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.GuessCount.Should().Be(0);
            newGame.HasWordBeenGuessed.Should().BeFalse();
            newGame.GuessedLetters.Should().BeEmpty();
        }

        [TestMethod]
        public void MakeGuess_AddUnguessedLetter_LetterAppearsInGuessedLetters()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('A');
            newGame.GuessedLetters.Should().Contain('A');
        }

        [TestMethod]
        public void MakeGuess_AddLowercaseLetter_UppercaseLetterIsInGuessedLetters()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('a');
            newGame.GuessedLetters.Should().Contain('A');
        }

        [TestMethod]
        public void MakeGuess_AddLowercaseLetter_LowercaseLetterIsNotInGuessedLetters()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('a');
            newGame.GuessedLetters.Should().NotContain('a');
        }

        [TestMethod]
        public void HasLetterBeenGuessed_LowercaseLetterGuessed_UppercaseLetterReturnsTrue()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('a');
            newGame.HasLetterBeenGuessed('A').Should().BeTrue();
        }

        [TestMethod]
        public void HasLetterBeenGuessed_LowercaseLetterGuessed_LowercaseLetterReturnsTrue()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('a');
            newGame.HasLetterBeenGuessed('a').Should().BeTrue();
        }

        [TestMethod]
        public void GetMaskedWord_WithLettersGuessed_ReturnsMaskedValue()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('z');
            newGame.MakeGuess('e');

            newGame.MaskedWord.Should().Be("Z E _ _ _");
        }

        [TestMethod]
        public void GetMaskedWord_WordWithRepeatingLetter_ReturnsCorrectMaskedValue()
        {
            var newGame = new HangmanGame("Banana");

            newGame.MakeGuess('b');
            newGame.MakeGuess('a');

            newGame.MaskedWord.Should().Be("B A _ A _ A");
        }

        [TestMethod]
        public void HasWordBeenGuessed_OnceWordHasBeenGuessed_ReturnsTrue()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('z');
            newGame.MakeGuess('e');
            newGame.MakeGuess('r');
            newGame.MakeGuess('B');
            newGame.MakeGuess('A');

            newGame.HasWordBeenGuessed.Should().BeTrue();
        }

        [TestMethod]
        public void HasWordBeenGuessed_WordNotGUessedYet_ReturnsFalse()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('z');
            newGame.MakeGuess('e');

            newGame.HasWordBeenGuessed.Should().BeFalse();
        }

        [TestMethod]
        public void HasWordBeenGuessed_NoLettersGuessed_ReturnsFalse()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.HasWordBeenGuessed.Should().BeFalse();
        }

        [TestMethod]
        public void HasWordBeenGuessed_CorrectAndIncorrectLettersGuessed_ReturnsFalse()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('a');
            newGame.MakeGuess('c');
            newGame.MakeGuess('g');
            newGame.MakeGuess('y');

            newGame.HasWordBeenGuessed.Should().BeFalse();
        }

        [TestMethod]
        public void GetMaskedWord_NoGuessesMade_ReturnsCorrectMaskedValue()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MaskedWord.Should().Be("_ _ _ _ _");
        }

        [TestMethod]
        public void GetMaskedWord_OnlyIncorrectGuesses_ReturnsCorrectMaskedValue()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('x');
            newGame.MakeGuess('y');

            newGame.MaskedWord.Should().Be("_ _ _ _ _");
        }

        [TestMethod]
        public void GetMaskedWord_CorrectAndIncorrectGuesses_ReturnsCorrectMaskedValue()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('a');
            newGame.MakeGuess('d');
            newGame.MakeGuess('e');

            newGame.MaskedWord.Should().Be("_ E _ _ A");
        }

        [TestMethod]
        public void MakeGuess_LettersAreGuessed_GuessCountIncreases()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('a');
            newGame.MakeGuess('b');

            newGame.GuessCount.Should().Be(2);
        }

        [TestMethod]
        public void MakeGuess_AddGuessedLetter_ExceptionShouldBeThrown()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('A');
            newGame.MakeGuess('B');
            Action addLetterAction = () => { newGame.MakeGuess('A'); };
            addLetterAction.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void MakeGuess_MakingGuessAfterWordHasBeenGuessed_ExceptionShouldBeThrown()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('z');
            newGame.MakeGuess('e');
            newGame.MakeGuess('b');
            newGame.MakeGuess('r');
            newGame.MakeGuess('a');
            Action makeGuessAction = () => { newGame.MakeGuess('y'); };

            makeGuessAction.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void MakeGuess_AddLowercaseVersionOfGuessedLetter_ExceptionSHouldBeThrown()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('A');
            newGame.MakeGuess('B');
            Action addLetterAction = () => { newGame.MakeGuess('a'); };
            addLetterAction.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void UnguessedLetters_WithNoLettersGuessed_ReturnsAllLetters()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.UnguessedLetters.Should().BeEquivalentTo(HangmanUtilities.AllLetters);
        }

        [TestMethod]
        public void IncorrectLetterCount_WithNoLettersGuessed_ReturnsZero()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.IncorrectGuessCount.Should().Be(0);
        }

        [TestMethod]
        public void IncorrectLetterCount_WithOnlyIncorrectGuesses_ShouldReturnCorrectCount()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('w');
            newGame.MakeGuess('d');

            newGame.IncorrectGuessCount.Should().Be(2);
        }

        [TestMethod]
        public void GuessesRemaining_WithOnlyCorrectGuesses_ShouldBeMaxGuesses()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('z');
            newGame.MakeGuess('e');

            newGame.GuessesRemaining.Should().Be(newGame.MaxGuesses);
        }

        [TestMethod]
        public void GuessesRemaining_WithIncorrectGuesses_ShouldDecrementCorrectly()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('z');
            newGame.MakeGuess('d');
            newGame.MakeGuess('y');

            newGame.GuessesRemaining.Should().Be(8);
        }

        [TestMethod]
        public void IncorrectLetterCount_WithGoodAndBadGuesses_ShouldReturnCorrectCount()
        {
            var newGame = new HangmanGame("Zebra");

            newGame.MakeGuess('z');
            newGame.MakeGuess('e');
            newGame.MakeGuess('d');

            newGame.IncorrectGuessCount.Should().Be(1);
        }

        [TestMethod]
        public void UnguessedLetters_AfterGuessingLetters_ShouldNotContainGuessedLetters()
        {
            var newGame = new HangmanGame("Zebra");
            var expectedResult = new SortedSet<char> {'B','C', 'D', 'F', 'G', 'H',
             'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'S', 'U', 'V',
             'W', 'X', 'Y', 'Z'};

            newGame.MakeGuess('a');
            newGame.MakeGuess('e');
            newGame.MakeGuess('t');
            newGame.MakeGuess('r');

            newGame.UnguessedLetters.Should().BeEquivalentTo(expectedResult);
        }
    }
}
