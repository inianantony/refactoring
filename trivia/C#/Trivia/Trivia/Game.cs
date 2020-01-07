using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    public class GamePlayers
    {
        public readonly List<Player> _gamePlayers = new List<Player>();
        public int _currentPlayer;
        public List<string> _players => _gamePlayers.Select(a => a.PlayerName).ToList();

        public int[] _places = new int[6];

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

        public string CurrentPlayerName => _gamePlayers[_currentPlayer].PlayerName;

        public int PlayerCount => _players.Count;

        public void InitPurses() => this._purses[this.PlayerCount] = 0;

        public void InitPlaces() => this._places[this.PlayerCount] = 0;

        public void InitPenalty() => this._inPenaltyBox[this.PlayerCount] = false;

        public void InitState()
        {
            this.InitPlaces();
            this.InitPurses();
            this.InitPenalty();
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
            Console.WriteLine("They are player number " + _gamePlayers._players.Count);
            return true;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(_gamePlayers.CurrentPlayerName + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_gamePlayers._inPenaltyBox[_gamePlayers._currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_gamePlayers.CurrentPlayerName + " is getting out of the penalty box");
                    _gamePlayers._places[_gamePlayers._currentPlayer] = _gamePlayers._places[_gamePlayers._currentPlayer] + roll;
                    if (_gamePlayers._places[_gamePlayers._currentPlayer] > 11) _gamePlayers._places[_gamePlayers._currentPlayer] = _gamePlayers._places[_gamePlayers._currentPlayer] - 12;

                    Console.WriteLine(_gamePlayers.CurrentPlayerName
                                      + "'s new location is "
                                      + _gamePlayers._places[_gamePlayers._currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
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

                _gamePlayers._places[_gamePlayers._currentPlayer] = _gamePlayers._places[_gamePlayers._currentPlayer] + roll;
                if (_gamePlayers._places[_gamePlayers._currentPlayer] > 11) _gamePlayers._places[_gamePlayers._currentPlayer] = _gamePlayers._places[_gamePlayers._currentPlayer] - 12;

                Console.WriteLine(_gamePlayers.CurrentPlayerName
                                  + "'s new location is "
                                  + _gamePlayers._places[_gamePlayers._currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }

        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }


        private String CurrentCategory()
        {
            if (_gamePlayers._places[_gamePlayers._currentPlayer] == 0) return "Pop";
            if (_gamePlayers._places[_gamePlayers._currentPlayer] == 4) return "Pop";
            if (_gamePlayers._places[_gamePlayers._currentPlayer] == 8) return "Pop";
            if (_gamePlayers._places[_gamePlayers._currentPlayer] == 1) return "Science";
            if (_gamePlayers._places[_gamePlayers._currentPlayer] == 5) return "Science";
            if (_gamePlayers._places[_gamePlayers._currentPlayer] == 9) return "Science";
            if (_gamePlayers._places[_gamePlayers._currentPlayer] == 2) return "Sports";
            if (_gamePlayers._places[_gamePlayers._currentPlayer] == 6) return "Sports";
            if (_gamePlayers._places[_gamePlayers._currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (_gamePlayers._inPenaltyBox[_gamePlayers._currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _gamePlayers._purses[_gamePlayers._currentPlayer]++;
                    Console.WriteLine(_gamePlayers.CurrentPlayerName
                                      + " now has "
                                      + _gamePlayers._purses[_gamePlayers._currentPlayer]
                                      + " Gold Coins.");

                    bool winner = DidPlayerWin();
                    _gamePlayers._currentPlayer++;
                    if (_gamePlayers._currentPlayer == _gamePlayers._players.Count) _gamePlayers._currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _gamePlayers._currentPlayer++;
                    if (_gamePlayers._currentPlayer == _gamePlayers._players.Count) _gamePlayers._currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _gamePlayers._purses[_gamePlayers._currentPlayer]++;
                Console.WriteLine(_gamePlayers.CurrentPlayerName
                                  + " now has "
                                  + _gamePlayers._purses[_gamePlayers._currentPlayer]
                                  + " Gold Coins.");

                bool winner = DidPlayerWin();
                _gamePlayers._currentPlayer++;
                if (_gamePlayers._currentPlayer == _gamePlayers._players.Count) _gamePlayers._currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_gamePlayers.CurrentPlayerName + " was sent to the penalty box");
            _gamePlayers._inPenaltyBox[_gamePlayers._currentPlayer] = true;

            _gamePlayers._currentPlayer++;
            if (_gamePlayers._currentPlayer == _gamePlayers._players.Count) _gamePlayers._currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return !(_gamePlayers._purses[_gamePlayers._currentPlayer] == 6);
        }
    }

    public class Player
    {
        public string PlayerName { get; }

        public Player(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
