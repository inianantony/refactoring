using NUnit.Framework;
using TechTalk.SpecFlow;
using Trivia.Models;
using Trivia;
using Trivia.Services;

namespace Trivia.Scenarios.Steps
{
    [Binding]
    public class StepDefinition
    {
        private readonly Game _game = new Game();

        private bool _result = true;

        [Given(@"Player ""(.*)"" have joined the game")]
        public void GivenPlayerHaveJoinedTheGame(string playerName)
        {
            _game.Add(new Player(playerName));
        }

        [Given(@"Rolled (.*) during the game play")]
        public void GivenRolledDuringTheGamePlay(int roll)
        {
            _game.Roll(new Roll(roll));
        }

        [When(@"Rolled (.*) during the game play")]
        public void WhenRolledDuringTheGamePlay(int roll)
        {
            GivenRolledDuringTheGamePlay(roll);
        }

        [When(@"Player answers the question correctly")]
        public void WhenPlayerAnswersTheQuestionCorrectly()
        {
            _game.AnswerCorrectly();
            _result = !_game.HasCurrentPlayerWon();
            
            _game.MoveToNextPlayer();
        }

        [When(@"Player answers the question wrongly")]
        public void WhenPlayerAnswersTheQuestionWrongly()
        {
            _game.AnswerWrongly();
            _result = !_game.HasCurrentPlayerWon();

            _game.MoveToNextPlayer();
        }

        [When(@"Repeat rolling (.*) and answering correctly for (.*) more times")]
        public void WhenRepeatRollingAndAnsweringCorrectlyForMoreTimes(int roll, int times)
        {
            for (int i = 1; i <= times; i++)
            {
                GivenRolledDuringTheGamePlay(roll);
                WhenPlayerAnswersTheQuestionCorrectly();
            }
        }

        [Then(@"The last correct answer should return ""(.*)""")]
        public void ThenTheLastCorrectAnswerShouldReturn(string expectedString)
        {
            var expected = expectedString == "true";
            Assert.AreEqual(expected, _result);
        }



    }
}
