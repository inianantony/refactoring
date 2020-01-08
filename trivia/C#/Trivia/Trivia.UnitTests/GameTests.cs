using NUnit.Framework;
using Trivia.Models;
using UglyTrivia;

namespace Trivia.UnitTests
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
        public void WrongAnswer_ShouldAlwaysReturn_True()
        {
            _game.Add(new Player("Player 1"));

            _game.AnswerWrongly();
            var didPlayerWin = _game.HasCurrentPlayerWon();
            _game.MoveToNextPlayer();
            var actual = !didPlayerWin;
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void WasCorrectlyAnswered_ShouldReturn_True()
        {
            _game.Add(new Player("Player 1"));

            _game.AnswerCorrectly();
            var didPlayerWin = _game.HasCurrentPlayerWon();
            _game.MoveToNextPlayer();
            var actual = !didPlayerWin;
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void WasCorrectlyAnswered_ShouldReturn_False_For_6th_Call()
        {
            _game.Add(new Player("Player 1"));

            //Make 5 calls
            for (var i = 1; i <= 5; i++)
            {
                _game.AnswerCorrectly();
                var didPlayerWin = _game.HasCurrentPlayerWon();
                _game.MoveToNextPlayer();
                bool temp = !didPlayerWin;
            }

            //Make 6th call
            _game.AnswerCorrectly();
            var didPlayerWin1 = _game.HasCurrentPlayerWon();
            _game.MoveToNextPlayer();
            bool actual = !didPlayerWin1;

            Assert.AreEqual(false, actual);
        }

        [Test]
        public void WasCorrectlyAnswered_ShouldReturn_True_For_7th_Call()
        {
            _game.Add(new Player("Player 1"));

            //Make 5 calls
            for (var i = 1; i <= 6; i++)
            {
                _game.AnswerCorrectly();
                var didPlayerWin = _game.HasCurrentPlayerWon();
                _game.MoveToNextPlayer();
                bool temp = !didPlayerWin;
            }

            //Make 6th call
            _game.AnswerCorrectly();
            var didPlayerWin1 = _game.HasCurrentPlayerWon();
            _game.MoveToNextPlayer();
            bool actual = !didPlayerWin1;

            Assert.AreEqual(true, actual);
        }

    }
}
