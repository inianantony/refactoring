using System;
using Trivia.Models;

namespace Trivia.Services
{
    public class CorrectAnswerBehaviour
    {
        private readonly GamePlayers _gamePlayers;

        public CorrectAnswerBehaviour(GamePlayers gamePlayers)
        {
            _gamePlayers = gamePlayers;
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
            Console.WriteLine($"{_gamePlayers.CurrentPlayerName} now has {_gamePlayers.CurrentPlayerPoints} Gold Coins.");
        }

        private static void CorrectAnswerMsg()
        {
            Console.WriteLine("Answer was correct!!!!");
        }
    }
}