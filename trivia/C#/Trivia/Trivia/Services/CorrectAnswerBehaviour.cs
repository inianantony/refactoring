using System;
using Trivia.Models;

namespace Trivia.Services
{
    public class CorrectAnswerBehaviour
    {
        private readonly GamePlayers _gamePlayers;
        private readonly GameLogger _gameLogger;

        public CorrectAnswerBehaviour(GamePlayers gamePlayers, GameLogger gameLogger)
        {
            _gamePlayers = gamePlayers;
            _gameLogger = gameLogger;
        }

        public bool MakeCorrectAnswer()
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
    }
}