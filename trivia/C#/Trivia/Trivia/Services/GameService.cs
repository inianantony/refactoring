using System;
using Trivia.Interfaces;
using Trivia.Models;

namespace Trivia.Services
{
    public class GameService
    {
        private bool _hasPlayerWon;
        private readonly IGame _aGame;
        private readonly IRandomizer _rand;

        public GameService() : this(new Game(), new Randomizer(new Random()))
        {

        }

        public GameService(Game game, IRandomizer random)
        {
            _aGame = game;
            _rand = random;

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

        public int NextRandomNumber(int maxVal)
        {
            return _rand.NextRandomNumber(maxVal);
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