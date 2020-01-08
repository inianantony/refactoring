using System;
using Trivia.Models;

namespace Trivia.Services
{
    public class WrongAnswerBehaviour
    {
        private readonly GamePlayers _gamePlayers;

        public WrongAnswerBehaviour(GamePlayers gamePlayers)
        {
            _gamePlayers = gamePlayers;
        }

        public bool MakeWrongAnswer()
        {
            WrongAnswerMessage();

            _gamePlayers.GivePenaltyToCurrentPlayer();
            Console.WriteLine($"{_gamePlayers.CurrentPlayerName} was sent to the penalty box");

            _gamePlayers.MoveToNextPlayer();
            return true;
        }

        private static void WrongAnswerMessage()
        {
            Console.WriteLine("Question was incorrectly answered");
        }
    }
}