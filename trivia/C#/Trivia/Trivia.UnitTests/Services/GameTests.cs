using NUnit.Framework;
using Trivia.Models;
using Trivia.Services;

namespace Trivia.UnitTests.Services
{
    [TestFixture]
    public class GameTests
    {
        private Game _game;
        [SetUp]
        public void Init()
        {
            _game = new Game();
        }

        [TestCase("", Description = "Empty Name")]
        [TestCase(null, Description = "pass name as null")]
        [TestCase("Name", Description = "pass valid name")]
        public void Add_Should_Return_True(string name)
        {
            var actual = _game.Add(new Player(name));
            Assert.AreEqual(true, actual);
        }

        [TestCase(0, Description = "pass roll value 0")]
        [TestCase(1, Description = "pass roll value 1")]
        [TestCase(10, Description = "pass roll value 10")]
        [TestCase(-1, Description = "pass roll value -1")]
        [TestCase(10000, Description = "pass roll value 10000")]
        public void Roll_ShouldNotThrowException(int roll)
        {
            _game.Add(new Player("Player 1"));
            Assert.DoesNotThrow(() =>
            {
                _game.Roll(new Roll(roll));
            });
        }

        [Test]
        public void HasCurrentPlayerWon_ShouldAlwaysReturn_False_WhenPlayerAnswersWrong()
        {
            _game.Add(new Player("Player 1"));

            _game.AnswerWrongly();

            var actual = _game.HasCurrentPlayerWon();

            Assert.AreEqual(false, actual);
        }

        [Test]
        public void HasCurrentPlayerWon_ShouldReturn_False_WhenPlayerAnswerCorrectlyForLessThan6Times()
        {
            _game.Add(new Player("Player 1"));

            _game.AnswerCorrectly();

            var actual = _game.HasCurrentPlayerWon();

            Assert.AreEqual(false, actual);
        }

        [Test]
        public void HasCurrentPlayerWon_ShouldReturn_True_For_PlayerAnsweringCorrectlyFor_6_Times()
        {
            _game.Add(new Player("Player 1"));

            //Make 5 calls
            for (var i = 1; i <= 6; i++)
            {
                _game.AnswerCorrectly();
            }

            var actual = _game.HasCurrentPlayerWon();

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void HasCurrentPlayerWon_ShouldReturn_False_For_PlayerAnsweringCorrectlyFor_7_Times()
        {
            _game.Add(new Player("Player 1"));

            //Make 5 calls
            for (var i = 1; i <= 7; i++)
            {
                _game.AnswerCorrectly();
            }

            var actual = _game.HasCurrentPlayerWon();

            Assert.AreEqual(false, actual);
        }

    }
}
