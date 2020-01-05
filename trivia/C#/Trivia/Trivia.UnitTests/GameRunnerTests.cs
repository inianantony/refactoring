using NUnit.Framework;

namespace Trivia.UnitTests
{
    [TestFixture]
    public class GameRunnerTests
    {
        [Test]
        public void Main_ShouldNotThrowAnyException()
        {
            Assert.DoesNotThrow(() => GameRunner.Main(null));
        }
    }
}
