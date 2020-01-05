using System;
using UglyTrivia;

namespace Trivia
{
    public class GameService
    {
        private static bool _notAWinner;

        public static void BeginGame()
        {
            Game aGame = new Game();

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");

            Random rand = new Random();

            do
            {
                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    _notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    _notAWinner = aGame.wasCorrectlyAnswered();
                }
            } while (_notAWinner);
        }
    }
}