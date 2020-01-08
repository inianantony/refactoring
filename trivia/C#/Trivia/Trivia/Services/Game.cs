using System;
using Trivia.Models;

namespace Trivia.Services
{
    public class Game
    {
        private readonly GamePlayers _gamePlayers;
        private readonly GameQuestions _gameQuestions;

        public Game()
        {
            _gamePlayers = new GamePlayers();
            _gameQuestions = new GameQuestions();
        }

        public bool Add(Player player)
        {
            _gamePlayers.AddPlayer(player);
            LogPlayerAddition(player);
            return true;
        }

        public void LogPlayerAddition(Player player)
        {
            Console.WriteLine(player.PlayerName + " was added");
            Console.WriteLine("They are player number " + _gamePlayers.PlayerCount);
        }

        public void Roll(Roll roll)
        {
            new RollBehaviour(_gamePlayers, _gameQuestions).MakeRollAction(roll);
        }

        public void AnswerCorrectly()
        {
            new CorrectAnswerBehaviour(_gamePlayers).MakeCorrectAnswer();
        }

        public void MoveToNextPlayer()
        {
            _gamePlayers.MoveToNextPlayer();
        }

        public bool HasCurrentPlayerWon()
        {
            return _gamePlayers.DidCurrentPlayerWin();
        }

        public void AnswerWrongly()
        {
            new WrongAnswerBehaviour(_gamePlayers).MakeWrongAnswer();
        }
    }
}
