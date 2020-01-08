using System;
using Trivia.Models;

namespace UglyTrivia
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

        public bool Add(string playerName, Player player)
        {
            _gamePlayers.AddPlayer(player);

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _gamePlayers.PlayerCount);
            return true;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(_gamePlayers.CurrentPlayerName + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_gamePlayers.CurrentPlayerIsInPenalty)
            {
                GrantOrRevokeLiberty(roll);
            }

            var canAskQuestion = !_gamePlayers.CurrentPlayerIsInPenalty || IsOddRoll(roll);
            if (canAskQuestion)
            {
                DoTheRolling(roll);
                Console.WriteLine("The category is " + _gameQuestions.CurrentCategory(_gamePlayers.CurrentPlayersPlace));
                _gameQuestions.AskQuestion(_gamePlayers.CurrentPlayersPlace);
            }
        }

        private void DoTheRolling(int roll)
        {
            _gamePlayers.MoveToRandomPlace(roll);
            LogTheRolling();
        }

        private void GrantOrRevokeLiberty(int roll)
        {
            var canGiveLiberty = _gamePlayers.CurrentPlayerIsInPenalty && IsOddRoll(roll);
            if (canGiveLiberty)
            {
                _gamePlayers.GiveLibertyForCurrentPlayer();
                Console.WriteLine(_gamePlayers.CurrentPlayerName + " is getting out of the penalty box");
            }
            else
            {
                Console.WriteLine(_gamePlayers.CurrentPlayerName + " is not getting out of the penalty box");
                _gamePlayers.NoLibertyForCurrentPlayer();
            }
        }

        private static bool IsOddRoll(int roll)
        {
            return roll % 2 != 0;
        }

        private void LogTheRolling()
        {
            Console.WriteLine($"{_gamePlayers.CurrentPlayerName}'s new location is {_gamePlayers.CurrentPlayersPlace}");
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
            LogGamePoint();
        }

        private static void CorrectAnswerMsg()
        {
            Console.WriteLine("Answer was correct!!!!");
        }

        private void LogGamePoint()
        {
            Console.WriteLine($"{_gamePlayers.CurrentPlayerName} now has {_gamePlayers.CurrentPlayerPoints} Gold Coins.");
        }

        public bool WrongAnswer()
        {
            WrongAnswerMessage();

            _gamePlayers.GivePenaltyToCurrentPlayer();
            LogSettingPenalty();

            _gamePlayers.MoveToNextPlayer();
            return true;
        }

        private void LogSettingPenalty()
        {
            Console.WriteLine($"{_gamePlayers.CurrentPlayerName} was sent to the penalty box");
        }

        private static void WrongAnswerMessage()
        {
            Console.WriteLine("Question was incorrectly answered");
        }
    }
}
