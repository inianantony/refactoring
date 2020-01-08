namespace Trivia.Models
{
    public class Roll
    {
        public Roll(int roll)
        {
            Value = roll;
        }

        public int Value { get; }

        public bool IsOddRoll()
        {
            return Value % 2 != 0;
        }
    }
}