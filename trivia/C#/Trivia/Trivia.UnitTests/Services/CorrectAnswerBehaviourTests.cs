using NUnit.Framework;
using Trivia.Models;
using Trivia.Services;

namespace Trivia.UnitTests.Services
{
    [TestFixture]
    public class CorrectAnswerBehaviourTests
    {
        [Test]
        public void MakeCorrectAnswer_ShouldAddOnePointToPlayer_IfPlayerDidntHavePenalty()
        {
            var gamePlayers = new GamePlayers();
            var player = new Player("A");
            gamePlayers.AddPlayer(player);
            CorrectAnswerBehaviour correctAnswerBehaviour = new CorrectAnswerBehaviour(gamePlayers);

            correctAnswerBehaviour.MakeCorrectAnswer();

            Assert.AreEqual(false, player.Penalty);
            Assert.AreEqual(1, gamePlayers.CurrentPlayerPoints);
        }

        [Test]
        public void MakeCorrectAnswer_Should_Not_AddPointToPlayer_IfPlayerHavePenalty()
        {
            var gamePlayers = new GamePlayers();
            var player = new Player("A");
            gamePlayers.AddPlayer(player);
            CorrectAnswerBehaviour correctAnswerBehaviour = new CorrectAnswerBehaviour(gamePlayers);
            player.SetPenalty();

            correctAnswerBehaviour.MakeCorrectAnswer();

            Assert.AreEqual(true, player.Penalty);
            Assert.AreEqual(0, gamePlayers.CurrentPlayerPoints);
        }
    }
}
