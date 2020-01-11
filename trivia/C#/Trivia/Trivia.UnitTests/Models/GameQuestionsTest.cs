using NUnit.Framework;
using Trivia.Models;

namespace Trivia.UnitTests.Models
{
    [TestFixture]
    public class GameQuestionsTest
    {
        private GameQuestions _gameQuestions;
        [SetUp]
        public void Init()
        {
            _gameQuestions = new GameQuestions();
        }

        [TestCase(0, ExpectedResult = "Pop")]
        [TestCase(1, ExpectedResult = "Science")]
        [TestCase(2, ExpectedResult = "Sports")]
        [TestCase(3, ExpectedResult = "Rock")]
        [TestCase(4, ExpectedResult = "Pop")]
        [TestCase(5, ExpectedResult = "Science")]
        [TestCase(6, ExpectedResult = "Sports")]
        [TestCase(7, ExpectedResult = "Rock")]
        [TestCase(8, ExpectedResult = "Pop")]
        [TestCase(9, ExpectedResult = "Science")]
        [TestCase(10, ExpectedResult = "Sports")]
        public string CurrentCategory_ShouldReturn_RespectiveCategory(int place)
        {
            return _gameQuestions.CurrentCategory(place);
        }

        [Test]
        public void CurrentCategory_ShouldReturn_Rock_WhenPlaceIsLessThan_0()
        {
            Assert.AreEqual("Rock", _gameQuestions.CurrentCategory(-1));
        }

        [Test]
        public void CurrentCategory_ShouldReturn_Rock_WhenPlaceIsGreaterThan_10()
        {
            Assert.AreEqual("Rock", _gameQuestions.CurrentCategory(11));
        }

        [Test]
        public void GetAQuestion_ShouldReturn_1stPopQuestionWhen()
        {
            Assert.AreEqual("Pop Question 0", _gameQuestions.GetAQuestion(0));
        }
    }
}
