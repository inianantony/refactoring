using System;
using Trivia.Models;
using UglyTrivia;

namespace Trivia
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
                DoRollAction();

                PlayerAnswersQuestion();

                DidPlayerWin();

                MoveToNextPlayer();

            } while (!_hasPlayerWon);
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
            if (_rand.Next(9) == 7)
            {
                _aGame.AnswerWrongly();
            }
            else
            {
                _aGame.AnswerCorrectly();
            }
        }

        private void DoRollAction()
        {
            int roll = _rand.Next(5) + 1;
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