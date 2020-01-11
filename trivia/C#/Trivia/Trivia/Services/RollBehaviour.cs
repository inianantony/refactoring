using System;
using Trivia.Models;

namespace Trivia.Services
{
    public class RollBehaviour
    {
        private readonly GamePlayers _gamePlayers;
        private readonly GameQuestions _gameQuestions;

        public RollBehaviour(GamePlayers gamePlayers, GameQuestions gameQuestions)
        {
            _gamePlayers = gamePlayers;
            _gameQuestions = gameQuestions;
        }

        public void MakeRollAction(Roll roll)
        {
            LogCurrentPlayer();

            LogRollValue(roll);

            ProcessLibertyAction(roll);

            if (CanAskQuestion(roll))
            {
                DoTheRolling(roll);
                AskTheQuestion();
            }
        }

        private void LogCurrentPlayer()
        {
            Console.WriteLine(_gamePlayers.CurrentPlayerName + " is the current player");
        }

        private static void LogRollValue(Roll roll)
        {
            Console.WriteLine("They have rolled a " + roll.Value);
        }

        private void ProcessLibertyAction(Roll roll)
        {
            if (!_gamePlayers.CurrentPlayerIsInPenalty) return;

            GrantOrRevokeLiberty(roll);
            LogLibertyMessage(roll);
        }

        private void GrantOrRevokeLiberty(Roll roll)
        {
            if (CanGiveLiberty(roll))
                _gamePlayers.GiveLibertyForCurrentPlayer();
            else
                _gamePlayers.NoLibertyForCurrentPlayer();
        }

        private void LogLibertyMessage(Roll roll)
        {
            bool canGiveLiberty = CanGiveLiberty(roll);
            Console.WriteLine($"{_gamePlayers.CurrentPlayerName} is{(canGiveLiberty ? "" : " not")} getting out of the penalty box");
        }

        private bool CanGiveLiberty(Roll roll)
        {
            return _gamePlayers.CurrentPlayerIsInPenalty && roll.IsOddRoll();
        }

        private void AskTheQuestion()
        {
            LogQuestionCategory();
            var question = _gameQuestions.GetAQuestion(_gamePlayers.CurrentPlayersPlace);
            Console.WriteLine(question);
        }

        private void LogQuestionCategory()
        {
            Console.WriteLine("The category is " + _gameQuestions.CurrentCategory(_gamePlayers.CurrentPlayersPlace));
        }

        private bool CanAskQuestion(Roll roll)
        {
            return !_gamePlayers.CurrentPlayerIsInPenalty || roll.IsOddRoll();
        }

        private void DoTheRolling(Roll roll)
        {
            _gamePlayers.MoveToRandomPlace(roll);
            LogRollLocation();
        }

        private void LogRollLocation()
        {
            Console.WriteLine($"{_gamePlayers.CurrentPlayerName}'s new location is {_gamePlayers.CurrentPlayersPlace}");
        }
    }
}