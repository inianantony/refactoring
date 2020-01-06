using NUnit.Framework;
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
            var actual = _game.Add(name);
            Assert.AreEqual(true, actual);
        }

        [TestCase(0, Description = "pass roll value 0")]
        [TestCase(1, Description = "pass roll value 1")]
        [TestCase(10, Description = "pass roll value 10")]
        [TestCase(-1, Description = "pass roll value -1")]
        [TestCase(10000, Description = "pass roll value 10000")]
        public void Roll_ShouldNotThrowException(int roll)
        {
            _game.Add("Player 1");
            Assert.DoesNotThrow(() =>
            {
                _game.Roll(roll);
            });
        }

        [Test]
        public void WrongAnswer_ShouldAlwaysReturnTrue()
        {
            _game.Add("Player 1");

            var actual = _game.wrongAnswer();
            Assert.AreEqual(true, actual);
        }
    }
}
