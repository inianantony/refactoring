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

            aGame.Add(new Player("Chet"));
            aGame.Add(new Player("Pat"));
            aGame.Add(new Player("Sue"));

            Random rand = new Random();

            do
            {
                int roll = rand.Next(5) + 1;
                aGame.Roll(new Roll(roll));

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