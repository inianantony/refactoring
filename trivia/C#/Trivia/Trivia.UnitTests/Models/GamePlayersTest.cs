using NUnit.Framework;
using Trivia.Models;
using ExpectedObjects;

namespace Trivia.UnitTests.Models
{
    [TestFixture]
    public class GamePlayersTest
    {
        private GamePlayers _gamePlayers;

        [SetUp]
        public void Init()
        {
            _gamePlayers = new GamePlayers();
        }

        [Test]
        public void GivenThereAre2Players_And_CurrentPlayer_ShouldReturnTheFirstPlayerAddedForTheFirstTime()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            var expected = new Player("A").ToExpectedObject();

            expected.ShouldMatch(_gamePlayers.CurrentPlayer);
        }

        [Test]
        public void GivenThereAre2Players_And_CurrentPlayer_ShouldReturnTheSecondPlayerAfterCalling_MoveToNextPlayer()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            _gamePlayers.MoveToNextPlayer();

            var expected = new Player("B").ToExpectedObject();

            expected.ShouldMatch(_gamePlayers.CurrentPlayer);
        }

        [Test]
        public void GivenThereAre2Players_And_CurrentPlayer_ShouldReturnTheFirstPlayerAfterCalling_MoveToNextPlayer_TwoTimes()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            _gamePlayers.MoveToNextPlayer();
            _gamePlayers.MoveToNextPlayer();

            var expected = new Player("A").ToExpectedObject();

            expected.ShouldMatch(_gamePlayers.CurrentPlayer);
        }

        [Test]
        public void GivenThereAre2Players_And_PlayerCountShouldReturn_2()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));


            Assert.AreEqual(2, _gamePlayers.PlayerCount);
        }

        [Test]
        public void GivenThereAre2Players_And_CurrentPlayerName_ShouldReturnTheFirstPlayers_Name()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            Assert.AreEqual("A", _gamePlayers.CurrentPlayerName);
        }

        [Test]
        public void GivenThereAre2Players_And_CurrentPlayerPenalty_ShouldReturnTheFirstPlayers_Penalty_When_GivePenaltyToCurrentPlayer_IsCalled()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            _gamePlayers.GivePenaltyToCurrentPlayer();

            Assert.AreEqual(true, _gamePlayers.CurrentPlayerIsInPenalty);
        }

        [Test]
        public void GivenThereAre2Players_And_CurrentPlayersPlace_ShouldReturnTheFirstPlayers_Position_When_MoveToRandomPlace_IsCalled()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            _gamePlayers.MoveToRandomPlace(new Roll(3));

            Assert.AreEqual(3, _gamePlayers.CurrentPlayersPlace);
        }

        [Test]
        public void GivenThereAre2Players_And_CurrentPlayerPoints_ShouldReturnTheFirstPlayers_Points_When_AddPointToCurrentPlayer_IsCalled()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            _gamePlayers.AddPointToCurrentPlayer();

            Assert.AreEqual(1, _gamePlayers.CurrentPlayerPoints);
        }

        [Test]
        public void GivenThereAre2Players_And_DidCurrentPlayerWin_ShouldReturnTheFirstPlayers_WinningState()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            _gamePlayers.AddPointToCurrentPlayer();
            _gamePlayers.AddPointToCurrentPlayer();
            _gamePlayers.AddPointToCurrentPlayer();
            _gamePlayers.AddPointToCurrentPlayer();
            _gamePlayers.AddPointToCurrentPlayer();
            _gamePlayers.AddPointToCurrentPlayer();

            Assert.AreEqual(true, _gamePlayers.DidCurrentPlayerWin());
        }

        [Test]
        public void GivenThereAre2Players_And_CanPlayerAnswer_ShouldReturn_False_When_CurrentPlayerIsInPenalty()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            _gamePlayers.GivePenaltyToCurrentPlayer();


            Assert.AreEqual(false, _gamePlayers.CanPlayerAnswer());
        }

        [Test]
        public void GivenThereAre2Players_And_CanPlayerAnswer_ShouldReturn_True_When_CurrentPlayerIsInPenalty_But_LibertyIsGiven()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            _gamePlayers.GivePenaltyToCurrentPlayer();
            _gamePlayers.GiveLibertyForCurrentPlayer();


            Assert.AreEqual(true, _gamePlayers.CanPlayerAnswer());
        }

        [Test]
        public void GivenThereAre2Players_And_CanPlayerAnswer_ShouldReturn_True_When_CurrentPlayerIsNotInPenalty()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            Assert.AreEqual(true, _gamePlayers.CanPlayerAnswer());
        }

        [Test]
        public void GivenThereAre2Players_And_CanPlayerAnswer_ShouldReturn_True_When_CurrentPlayerIsInPenalty_But_LibertyIsGiven_AndRevokedLater()
        {
            _gamePlayers.AddPlayer(new Player("A"));
            _gamePlayers.AddPlayer(new Player("B"));

            _gamePlayers.GivePenaltyToCurrentPlayer();
            _gamePlayers.GiveLibertyForCurrentPlayer();

            _gamePlayers.NoLibertyForCurrentPlayer();


            Assert.AreEqual(false, _gamePlayers.CanPlayerAnswer());
        }
    }
}
