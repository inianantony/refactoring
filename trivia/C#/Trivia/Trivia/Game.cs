using System;
using Trivia.Models;

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
            _gameLogger.LogIntroToRolling(roll, _gamePlayers);

            if (_gamePlayers.CurrentPlayerIsInPenalty)
            {
                GrantOrRevokeLiberty(roll);
            }

            var canAskQuestion = !_gamePlayers.CurrentPlayerIsInPenalty || IsOddRoll(roll);
            if (canAskQuestion)
            {
                DoTheRolling(roll);
                _gameLogger.LogQuestionCategory(_gameQuestions, _gamePlayers);
                _gameQuestions.AskQuestion(_gamePlayers.CurrentPlayersPlace);
            }
        }

        private void DoTheRolling(int roll)
        {
            _gamePlayers.MoveToRandomPlace(roll);
            _gameLogger.LogTheRolling(_gamePlayers);
        }

        private void GrantOrRevokeLiberty(int roll)
        {
            var canGiveLiberty = _gamePlayers.CurrentPlayerIsInPenalty && IsOddRoll(roll);
            if (canGiveLiberty)
                _gamePlayers.GiveLibertyForCurrentPlayer();
            else
                _gamePlayers.NoLibertyForCurrentPlayer();

            var msgToggler = canGiveLiberty ? "" : " not";
            var msg = _gameLogger.LogGrantOrRevokeLiberty(msgToggler, _gamePlayers);
            Console.WriteLine(msg);
        }

        private static bool IsOddRoll(int roll)
        {
            return roll % 2 != 0;
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
            return !didPlayerWin; ;
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
