using System;
using Trivia.Models;
using UglyTrivia;

namespace Trivia
{
    public class GameService
    {
        private bool _notAWinner;

        public void BeginGame()
        {
            Game aGame = new Game();

            aGame.Add("Chet", new Player("Chet"));
            aGame.Add("Pat", new Player("Pat"));
            aGame.Add("Sue", new Player("Sue"));

            Random rand = new Random();

            do
            {
                aGame.Roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    _notAWinner = aGame.WrongAnswer();
                }
                else
                {
                    _notAWinner = aGame.WasCorrectlyAnswered();
                }
            } while (_notAWinner);
        }
    }
}