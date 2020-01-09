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

        [Test]
        public void MoveToPlace_Should_AddRollValueToExistingPlaceValue()
        {
            var player = new Player("A");

            player.MoveToPlace(new Roll(3));

            Assert.AreEqual(3,player.Place);

            player.MoveToPlace(new Roll(5));

            Assert.AreEqual(8, player.Place);
        }

        [Test]
        public void AddPoint_Should_Add_One_ToExistingValue()
        {
            var player = new Player("A");

            player.AddPoint();

            Assert.AreEqual(1, player.Point);

            player.AddPoint();

            Assert.AreEqual(2, player.Point);

        }

        [Test]
        public void SetPenalty_Should_SetPenaltyValueToTrue()
        {
            var player = new Player("A");

            Assert.AreEqual(false, player.Penalty);

            player.SetPenalty();

            Assert.AreEqual(true, player.Penalty);

        }

        [Test]
        public void GiveLiberty_Should_SetLibertyToTrue()
        {
            var player = new Player("A");

            player.GiveLiberty();

            Assert.AreEqual(true, player.Liberty);
        }

        [Test]
        public void RevokeLiberty_Should_SetLibertyToFalse()
        {
            var player = new Player("A");

            player.GiveLiberty();


            player.RevokeLiberty();

            Assert.AreEqual(false, player.Liberty);
        }
    }
}
