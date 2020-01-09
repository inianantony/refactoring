using System;
using Trivia.Interfaces;

namespace Trivia.Services
{
    public class Randomizer : IRandomizer
    {
        private readonly Random _rand;

        public Randomizer(Random rand)
        {
            _rand = rand;
        }

        public int NextRandomNumber(int maxVal)
        {
            return _rand.Next(maxVal);
        }
    }
}