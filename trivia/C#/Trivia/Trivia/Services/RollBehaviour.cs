using System;
using Trivia.Models;

namespace Trivia.Services
{
    public class RollBehaviour
    {
        private readonly GamePlayers _gamePlayers;
        private readonly GameQuestions _gameQuestions;
        private readonly GameLogger _gameLogger;

        public RollBehaviour(GamePlayers gamePlayers, GameQuestions gameQuestions, GameLogger gameLogger)
        {
            _gamePlayers = gamePlayers;
            _gameQuestions = gameQuestions;
            _gameLogger = gameLogger;
        }

        public void MakeRollAction(int roll1)
        {
            var roll = new Roll(roll1);
            _gameLogger.LogIntroToRolling(roll, _gamePlayers);

            if (_gamePlayers.CurrentPlayerIsInPenalty)
            {
                GrantOrRevokeLiberty(roll);
            }

            if (CanAskQuestion(roll))
            {
                DoTheRolling(roll);
                AskingTheQuestion();
            }
        }

        private void AskingTheQuestion()
        {
            _gameLogger.LogQuestionCategory(_gameQuestions, _gamePlayers);
            _gameQuestions.AskQuestion(_gamePlayers.CurrentPlayersPlace);
        }

        private bool CanAskQuestion(Roll roll)
        {
            return !_gamePlayers.CurrentPlayerIsInPenalty || roll.IsOddRoll();
        }

        private void DoTheRolling(Roll roll)
        {
            _gamePlayers.MoveToRandomPlace(roll);
            _gameLogger.LogTheRolling(_gamePlayers);
        }

        private void GrantOrRevokeLiberty(Roll roll)
        {
            var canGiveLiberty = _gamePlayers.CurrentPlayerIsInPenalty && roll.IsOddRoll();
            if (canGiveLiberty)
                _gamePlayers.GiveLibertyForCurrentPlayer();
            else
                _gamePlayers.NoLibertyForCurrentPlayer();

            var msgToggler = canGiveLiberty ? "" : " not";
            var msg = _gameLogger.LogGrantOrRevokeLiberty(msgToggler, _gamePlayers);
            Console.WriteLine(msg);
        }
    }
}