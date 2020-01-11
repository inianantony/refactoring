using NUnit.Framework;
using Trivia.Models;

namespace Trivia.UnitTests.Models
{
    [TestFixture]
    public class RollTests
    {
        private Roll _roll;

        [Test]
        public void Value_ShouldReturn_2_WhenRollSentIs_0()
        {
            _roll = new Roll(2);
            Assert.AreEqual(2, _roll.Value);
        }

        [Test]
        public void IsOddRoll_ShouldReturn_True_WhenRollValueIsOdd()
        {
            _roll = new Roll(3);
            Assert.AreEqual(true, _roll.IsOddRoll());
        }

        [Test]
        public void IsOddRoll_ShouldReturn_False_WhenRollValueIsEven()
        {
            _roll = new Roll(2);
            Assert.AreEqual(false, _roll.IsOddRoll());
        }
    }
}
