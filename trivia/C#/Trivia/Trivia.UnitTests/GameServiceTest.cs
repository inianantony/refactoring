using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Moq;
using NUnit.Framework;
using Trivia.Interfaces;
using Trivia.Models;
using Trivia.Services;

namespace Trivia.UnitTests
{
    [TestFixture]
    public class GameServiceTest
    {
        private Mock<IRandomizer> _moqRandomizer;
        private Mock<IGame> _moqGame;

        [SetUp]
        public void Ini()
        {
            _moqRandomizer = SetUpRandomizer();
            _moqGame = SetUpGame();
        }

        [Test]
        public void BeginGame_ShouldNotThrowAnyException()
        {
            Assert.DoesNotThrow(() => new GameService().BeginGame());
        }


        [Test]
        public void BeginGame_ShouldCallGameMethodsAndInRespectiveOrder_WhenSomePlayerWinsIn_1st_Call()
        {
            //Arrange
            var gameService = new GameService(_moqGame.Object, _moqRandomizer.Object);
            var expectedCalls = new List<string>
                    {"Add", "Add", "Add", "Roll", "AnswerCorrectly", "HasCurrentPlayerWon", "MoveToNextPlayer"}
                .ToExpectedObject();

            //Act
            gameService.BeginGame();

            //Assert
            _moqGame.Verify(a => a.Add(It.IsAny<Player>()), Times.Exactly(3));
            _moqGame.Verify(a => a.Roll(It.IsAny<Roll>()), Times.Exactly(1));
            _moqGame.Verify(a => a.AnswerCorrectly(), Times.Exactly(1));
            _moqGame.Verify(a => a.HasCurrentPlayerWon(), Times.Exactly(1));
            _moqGame.Verify(a => a.MoveToNextPlayer(), Times.Exactly(1));

            var methodCalls = _moqGame.Invocations.Select(a => a.Method.Name).ToList();
            expectedCalls.ShouldEqual(methodCalls);

        }

        [Test]
        public void BeginGame_ShouldCall_AnswerCorrectly_WhenRandomizerReturnsAny_Number_OtherThan_7()
        {
            //Arrange
            _moqRandomizer.Setup(a => a.NextRandomNumber(9)).Returns(1);
            var gameService = new GameService(_moqGame.Object, _moqRandomizer.Object);

            //Act
            gameService.BeginGame();

            //Assert
            _moqGame.Verify(a => a.AnswerCorrectly(), Times.Exactly(1));
        }

        [Test]
        public void BeginGame_ShouldCall_AnswerWrongly_WhenRandomizerReturns_7()
        {
            //Arrange
            _moqRandomizer.Setup(a => a.NextRandomNumber(9)).Returns(7);
            var gameService = new GameService(_moqGame.Object, _moqRandomizer.Object);

            //Act
            gameService.BeginGame();

            //Assert
            _moqGame.Verify(a => a.AnswerWrongly(), Times.Exactly(1));
        }

        [Test]
        public void BeginGame_ShouldCallGameMethodsAndInRespectiveOrder_WhenSomePlayerWinsIn_2nd_Call()
        {
            //Arrange
            var hasPlayerWon = false;
            _moqGame.Setup(a => a.HasCurrentPlayerWon()).Returns(() => hasPlayerWon).Callback(() => hasPlayerWon = true);
            var gameService = new GameService(_moqGame.Object, _moqRandomizer.Object);
            var expectedCalls = new List<string>
            {
                "Add", "Add", "Add", "Roll", "AnswerCorrectly", "HasCurrentPlayerWon", "MoveToNextPlayer", "Roll",
                "AnswerCorrectly", "HasCurrentPlayerWon", "MoveToNextPlayer"
            }.ToExpectedObject();

            //Act
            gameService.BeginGame();

            //Assert
            _moqGame.Verify(a => a.Add(It.IsAny<Player>()), Times.Exactly(3));
            _moqGame.Verify(a => a.Roll(It.IsAny<Roll>()), Times.Exactly(2));
            _moqGame.Verify(a => a.AnswerCorrectly(), Times.Exactly(2));
            _moqGame.Verify(a => a.HasCurrentPlayerWon(), Times.Exactly(2));
            _moqGame.Verify(a => a.MoveToNextPlayer(), Times.Exactly(2));


            var methodCalls = _moqGame.Invocations.Select(a => a.Method.Name).ToList();
            expectedCalls.ShouldEqual(methodCalls);

        }

        private Mock<IGame> SetUpGame()
        {
            var moqGame = new Mock<IGame>();
            moqGame.Setup(a => a.Add(It.IsAny<Player>()));
            moqGame.Setup(a => a.Roll(It.IsAny<Roll>()));
            moqGame.Setup(a => a.AnswerCorrectly());
            moqGame.Setup(a => a.HasCurrentPlayerWon()).Returns(true);
            moqGame.Setup(a => a.MoveToNextPlayer());
            return moqGame;
        }

        private Mock<IRandomizer> SetUpRandomizer()
        {
            var moqRandomizer = new Mock<IRandomizer>();
            moqRandomizer.Setup(a => a.NextRandomNumber(9)).Returns(1);
            return moqRandomizer;
        }
    }
}
