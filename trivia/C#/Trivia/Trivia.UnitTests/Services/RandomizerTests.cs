using System;
using NUnit.Framework;
using Trivia.Services;

namespace Trivia.UnitTests.Services
{
    [TestFixture]
    public class RandomizerTests
    {
        [Test]
        public void NextRandomNumber_ShouldReturnANumberGreaterThan_Minus_1()
        {
            Randomizer randomizer = new Randomizer(new Random());
            Assert.Greater(randomizer.NextRandomNumber(9), -1);
        }
    }
}
