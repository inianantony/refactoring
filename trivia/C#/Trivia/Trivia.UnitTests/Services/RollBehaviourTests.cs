using NUnit.Framework;
using Trivia.Models;
using Trivia.Services;

namespace Trivia.UnitTests.Services
{
    [TestFixture]
    public class RollBehaviourTests
    {
        [Test]
        public void GivenPlayerIsInPenalty_And_MakeRollAction_ShouldGiveLibertyWhenOddRollIsMade()
        {
            var gamePlayers = new GamePlayers();
            var player = new Player("A");
            gamePlayers.AddPlayer(player);
            GameQuestions gameQuestions = new GameQuestions();
            RollBehaviour rollBehaviour = new RollBehaviour(gamePlayers, gameQuestions);
            player.SetPenalty();

            Assert.AreEqual(false, player.Liberty);

            rollBehaviour.MakeRollAction(new Roll(3));

            Assert.AreEqual(true,player.Liberty);
        }

        [Test]
        public void GivenPlayerIsInPenalty_And_LibertyWasGivenPreviously_And_MakeRollAction_ShouldRevokeLibertyWhenEvenRollIsMade()
        {
            var gamePlayers = new GamePlayers();
            var player = new Player("A");
            gamePlayers.AddPlayer(player);
            GameQuestions gameQuestions = new GameQuestions();
            RollBehaviour rollBehaviour = new RollBehaviour(gamePlayers, gameQuestions);
            player.SetPenalty();
            player.GiveLiberty();

            Assert.AreEqual(true, player.Liberty);

            rollBehaviour.MakeRollAction(new Roll(2));

            Assert.AreEqual(false, player.Liberty);
        }

        [Test]
        public void GivenPlayerIsDidntHavePenalty_And_MakeRollAction_Should_AddRoll_ToPlayers_Place()
        {
            var gamePlayers = new GamePlayers();
            var player = new Player("A");
            gamePlayers.AddPlayer(player);
            GameQuestions gameQuestions = new GameQuestions();
            RollBehaviour rollBehaviour = new RollBehaviour(gamePlayers, gameQuestions);

            Assert.AreEqual(0, player.Place);

            rollBehaviour.MakeRollAction(new Roll(2));

            Assert.AreEqual(2, player.Place);
        }

        [Test]
        public void GivenPlayerIsInPenaltyy_And_MakeRollAction_Should_AddRoll_ToPlayers_Place_WhenPlayerMadeOddRoll()
        {
            var gamePlayers = new GamePlayers();
            var player = new Player("A");
            gamePlayers.AddPlayer(player);
            GameQuestions gameQuestions = new GameQuestions();
            RollBehaviour rollBehaviour = new RollBehaviour(gamePlayers, gameQuestions);
            player.SetPenalty();

            Assert.AreEqual(0, player.Place);

            rollBehaviour.MakeRollAction(new Roll(3));

            Assert.AreEqual(3, player.Place);
        }

        [Test]
        public void GivenPlayerIsInPenaltyy_And_MakeRollAction_Should_Not_AddRoll_ToPlayers_Place_WhenPlayerMadeEvenRoll()
        {
            var gamePlayers = new GamePlayers();
            var player = new Player("A");
            gamePlayers.AddPlayer(player);
            GameQuestions gameQuestions = new GameQuestions();
            RollBehaviour rollBehaviour = new RollBehaviour(gamePlayers, gameQuestions);
            player.SetPenalty();

            Assert.AreEqual(0, player.Place);

            rollBehaviour.MakeRollAction(new Roll(2));

            Assert.AreEqual(0, player.Place);
        }

    }
}
