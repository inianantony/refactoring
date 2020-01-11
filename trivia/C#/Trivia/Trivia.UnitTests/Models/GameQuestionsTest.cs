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

        [TestCase(0,ExpectedResult = "Pop")]
        public string CurrentCategory_ShouldReturn_RespectiveCategory(int place)
        {
            return _gameQuestions.CurrentCategory(place);
        }
    }
}
