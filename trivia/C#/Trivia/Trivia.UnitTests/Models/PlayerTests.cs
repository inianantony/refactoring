using NUnit.Framework;
using Trivia.Models;

namespace Trivia.UnitTests.Models
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void PlayerName_Should_Be_A_When_Constructed_A()
        {
            Assert.AreEqual("A", new Player("A").PlayerName);
        }

        [Test]
        public void Point_Should_Be_0_DuringConstruction()
        {
            Assert.AreEqual(0, new Player("A").Point);
        }

        [Test]
        public void Place_Should_Be_0_DuringConstruction()
        {
            Assert.AreEqual(0, new Player("A").Place);
        }

        [Test]
        public void Penalty_Should_Be_False_DuringConstruction()
        {
            Assert.AreEqual(false, new Player("A").Penalty);
        }

        [Test]
        public void Liberty_Should_Be_False_DuringConstruction()
        {
            Assert.AreEqual(false, new Player("A").Liberty);
        }
    }
}
