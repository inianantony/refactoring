using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    public class Game
    {
        readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        readonly LinkedList<string> _rockQuestions = new LinkedList<string>();


        private readonly GamePlayers _gamePlayers;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }

            _gamePlayers = new GamePlayers();
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
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
                Console.WriteLine("The category is " + CurrentCategory(_gamePlayers.CurrentPlayersPlace));
                AskQuestion();
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

        private void AskQuestion()
        {
            var currentCategory = CurrentCategory(_gamePlayers.CurrentPlayersPlace);
            if (currentCategory == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (currentCategory == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (currentCategory == "Sports")
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (currentCategory == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }


        private string CurrentCategory(int place)
        {
            if (place == 0) return "Pop";
            if (place == 4) return "Pop";
            if (place == 8) return "Pop";
            if (place == 1) return "Science";
            if (place == 5) return "Science";
            if (place == 9) return "Science";
            if (place == 2) return "Sports";
            if (place == 6) return "Sports";
            if (place == 10) return "Sports";
            return "Rock";
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
