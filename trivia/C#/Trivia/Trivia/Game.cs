using System;
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

        public bool Add(string playerName, Player player)
        {
            _gamePlayers.AddPlayer(player);
            _gameLogger.LogPlayerAddition(_gamePlayers, playerName);
            return true;
        }

        public void Roll(int roll)
        {
            new RollBehaviour(_gamePlayers, _gameQuestions, _gameLogger).MakeRollAction(roll);
        }

        public bool WasCorrectlyAnswered()
        {
            var playerCanAnswer = _gamePlayers.CanPlayerAnswer();
            if (playerCanAnswer)
            {
                AnswerCorrectly();
            }
            var didPlayerWin = _gamePlayers.DidCurrentPlayerWin();
            _gamePlayers.MoveToNextPlayer();
            return !didPlayerWin;
        }

        private void AnswerCorrectly()
        {
            CorrectAnswerMsg();
            _gamePlayers.AddPointToCurrentPlayer();
            _gameLogger.LogGamePoint(_gamePlayers);
        }

        private static void CorrectAnswerMsg()
        {
            Console.WriteLine("Answer was correct!!!!");
        }

        public bool WrongAnswer()
        {
            WrongAnswerMessage();

            _gamePlayers.GivePenaltyToCurrentPlayer();
            _gameLogger.LogSettingPenalty(_gamePlayers);

            _gamePlayers.MoveToNextPlayer();
            return true;
        }

        private static void WrongAnswerMessage()
        {
            Console.WriteLine("Question was incorrectly answered");
        }
    }
}
