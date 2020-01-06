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
    }
}
