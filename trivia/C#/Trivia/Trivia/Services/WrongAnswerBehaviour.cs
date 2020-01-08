using System;
using Trivia.Models;

namespace Trivia.Services
{
    public class WrongAnswerBehaviour
    {
        private readonly GamePlayers _gamePlayers;
        private readonly GameLogger _gameLogger;

        public WrongAnswerBehaviour(GamePlayers gamePlayers, GameLogger gameLogger)
        {
            _gamePlayers = gamePlayers;
            _gameLogger = gameLogger;
        }

        public bool MakeWrongAnswer()
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