using NUnit.Framework;
using TechTalk.SpecFlow;
using UglyTrivia;

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
            _game.Add(playerName);
        }

        [Given(@"Rolled (.*) during the game play")]
        public void GivenRolledDuringTheGamePlay(int roll)
        {
            _game.Roll(roll);
        }

        [When(@"Player answers the question correctly")]
        public void WhenPlayerAnswersTheQuestionCorrectly()
        {
            _result = _game.WasCorrectlyAnswered();
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


        [Then(@"The last correct answer should return false")]
        public void ThenTheLastCorrectAnswerShouldReturnFalse()
        {
            Assert.AreEqual(false, _result);
        }


    }
}
