using NUnit.Framework;
using Trivia.Models;
using Trivia.Services;

namespace Trivia.UnitTests.Services
{
    [TestFixture]
    public class CorrectAnswerBehaviourTests
    {
        [Test]
        public void MakeCorrectAnswer_ShouldAddOnePointToPlayer()
        {
            var gamePlayers = new GamePlayers();
            gamePlayers.AddPlayer(new Player("A"));
            CorrectAnswerBehaviour correctAnswerBehaviour = new CorrectAnswerBehaviour(gamePlayers);

            correctAnswerBehaviour.MakeCorrectAnswer();

            Assert.AreEqual(1, gamePlayers.CurrentPlayerPoints);
        }
    }
}
