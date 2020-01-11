using Moq;
using NUnit.Framework;
using Trivia.Models;
using Trivia.Services;

namespace Trivia.UnitTests.Services
{
    [TestFixture]
    public class WrongAnswerBehaviourTests
    {
        [Test]
        public void MakeWrongAnswer_ShouldCall_GivePenaltyToCurrentPlayer_FromGamePlayers()
        {
            var gamePlayers = new GamePlayers();
            gamePlayers.AddPlayer(new Player("A"));
            var wrongAnswerBehaviour = new WrongAnswerBehaviour(gamePlayers);

            Assert.AreEqual(false, gamePlayers.CurrentPlayerIsInPenalty);

            wrongAnswerBehaviour.MakeWrongAnswer();

            Assert.AreEqual(true, gamePlayers.CurrentPlayerIsInPenalty);
        }
    }
}
