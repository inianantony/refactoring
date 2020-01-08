using Trivia.Models;
using Trivia.Services;

namespace UglyTrivia
{
    public class Game
    {
        private readonly GamePlayers _gamePlayers;
        private readonly GameQuestions _gameQuestions;
        private readonly GameLogger _gameLogger;

        public Game()
        {
            _gamePlayers = new GamePlayers();
            _gameQuestions = new GameQuestions();
            _gameLogger = new GameLogger();
        }

        public bool Add(Player player)
        {
            _gamePlayers.AddPlayer(player);
            _gameLogger.LogPlayerAddition(_gamePlayers, player.PlayerName);
            return true;
        }

        public void Roll(int roll)
        {
            new RollBehaviour(_gamePlayers, _gameQuestions).MakeRollAction(new Roll(roll));
        }

        public bool WasCorrectlyAnswered()
        {
            return new CorrectAnswerBehaviour(_gamePlayers, _gameLogger).MakeCorrectAnswer();
        }

        public bool WrongAnswer()
        {
            return new WrongAnswerBehaviour(_gamePlayers, _gameLogger).MakeWrongAnswer();
        }
    }
}
