using System;

namespace Trivia
{
    public class GameRunner
    {
        public static void Main(String[] args)
        {
            new GameService().BeginGame();
        }
    }

}

