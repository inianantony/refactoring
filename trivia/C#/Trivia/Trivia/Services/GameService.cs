using System;
using Trivia.Models;

namespace Trivia.Services
{
    public class GameService
    {
        private bool _hasPlayerWon;
        private readonly Game _aGame;
        private readonly Random _rand;
        public GameService()
        {
            _aGame = new Game();
            _rand = new Random();

        }
        public void BeginGame()
        {
            AddGamePlayers();

            do
            {
                AddLineSeperator();

                DoRollAction();

                PlayerAnswersQuestion();

                DidPlayerWin();

                MoveToNextPlayer();
            } while (!_hasPlayerWon);
        }

        private void AddLineSeperator()
        {
            Console.WriteLine("");
            Console.WriteLine("=============================================");
            Console.WriteLine("");
        }

        private void MoveToNextPlayer()
        {
            _aGame.MoveToNextPlayer();
        }

        private void DidPlayerWin()
        {
            _hasPlayerWon = _aGame.HasCurrentPlayerWon();
        }

        private void PlayerAnswersQuestion()
        {
            if (NextRandomNumber(9) == 7)
            {
                _aGame.AnswerWrongly();
            }
            else
            {
                _aGame.AnswerCorrectly();
            }
        }

        private int NextRandomNumber(int maxVal)
        {
            return _rand.Next(maxVal);
        }

        private void DoRollAction()
        {
            int roll = NextRandomNumber(5) + 1;
            _aGame.Roll(new Roll(roll));
        }

        private void AddGamePlayers()
        {
            _aGame.Add(new Player("Chet"));
            _aGame.Add(new Player("Pat"));
            _aGame.Add(new Player("Sue"));
        }
    }
}