﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    public class GamePlayers
    {
        public readonly List<Player> _gamePlayers = new List<Player>();
        public int _currentPlayer;
        public List<string> _players => _gamePlayers.Select(a => a.PlayerName).ToList();

        public int[] _purses = new int[6];

        public bool[] _inPenaltyBox = new bool[6];

        public GamePlayers()
        {
            _currentPlayer = 0;
        }

        public void AddPlayer(Player player)
        {
            _gamePlayers.Add(player);
        }

        public string CurrentPlayerName => CurrentPlayer.PlayerName;
        public Player CurrentPlayer => _gamePlayers[_currentPlayer];

        public int PlayerCount => _players.Count;

        public void InitPurses() => _purses[PlayerCount] = 0;

        public void InitPenalty() => _inPenaltyBox[PlayerCount] = false;

        public void InitState()
        {
            InitPurses();
            InitPenalty();
        }

        public void MoveToRandomPlace(int roll)
        {
            CurrentPlayer.MoveToPlace(roll);
        }

        public bool CurrentPlayerIsInPenalty => _inPenaltyBox[_currentPlayer];

        public int CurrentPlayersPlace => CurrentPlayer.Place;

        public void MoveToNextPlayer()
        {
            _currentPlayer++;
            if (_currentPlayer == PlayerCount) _currentPlayer = 0;

        }

        public void AddPoint()
        {
            _purses[_currentPlayer]++;
        }

        public bool DidPlayerWin()
        {
            return _purses[_currentPlayer] == 6;
        }

        public void GivePenaltyToPlayer()
        {
            _inPenaltyBox[_currentPlayer] = true;
        }
    }

    public class Game
    {
        readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        readonly LinkedList<string> _rockQuestions = new LinkedList<string>();


        bool _isGettingOutOfPenaltyBox;
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

        public bool IsPlayable()
        {
            return (_gamePlayers.PlayerCount >= 2);
        }

        public bool Add(string playerName, Player player)
        {
            _gamePlayers.AddPlayer(player);
            _gamePlayers.InitState();

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
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_gamePlayers.CurrentPlayerName + " is getting out of the penalty box");
                    _gamePlayers.MoveToRandomPlace(roll);
                    LogTheRolling();

                    Console.WriteLine("The category is " + CurrentCategory(_gamePlayers.CurrentPlayersPlace));
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_gamePlayers.CurrentPlayerName + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {
                _gamePlayers.MoveToRandomPlace(roll);
                LogTheRolling();

                Console.WriteLine("The category is " + CurrentCategory(_gamePlayers.CurrentPlayersPlace));
                AskQuestion();
            }

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
            if (_gamePlayers.CurrentPlayerIsInPenalty)
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    AnswerCorrectly();

                    bool winner = !_gamePlayers.DidPlayerWin();
                    
                    _gamePlayers.MoveToNextPlayer();

                    return winner;
                }

                _gamePlayers.MoveToNextPlayer();
                return true;
            }

            {

                AnswerCorrectly();

                bool winner = !_gamePlayers.DidPlayerWin();
                _gamePlayers.MoveToNextPlayer();

                return winner;
            }
        }

        private void AnswerCorrectly()
        {
            CorrectAnswerMsg();
            _gamePlayers.AddPoint();
            LogGamePoint();
        }

        private static void CorrectAnswerMsg()
        {
            Console.WriteLine("Answer was correct!!!!");
        }

        private void LogGamePoint()
        {
            Console.WriteLine(_gamePlayers.CurrentPlayerName + " now has " + _gamePlayers._purses[_gamePlayers._currentPlayer] +
                              " Gold Coins.");
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_gamePlayers.CurrentPlayerName + " was sent to the penalty box");
            _gamePlayers.GivePenaltyToPlayer();

            _gamePlayers.MoveToNextPlayer();
            return true;
        }
    }

    public class Player
    {
        public string PlayerName { get; }
        public int Place { get; private set; }

        public Player(string playerName)
        {
            PlayerName = playerName;
        }

        public void MoveToPlace(int roll)
        {
            Place += roll;
            if (Place > 11)
                Place -= 12;
        }
    }
}
