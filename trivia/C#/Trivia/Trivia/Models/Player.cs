using Trivia.Services;

namespace Trivia.Models
{
    public class Player
    {
        public string PlayerName { get; }
        public int Place { get; private set; }
        public bool Penalty { get; private set; }
        public int Point { get; private set; }
        public bool Liberty { get; private set; }

        public bool IsWinner => Point == 6;

        public Player(string playerName)
        {
            PlayerName = playerName;
            Point = 0;
            Place = 0;
            Penalty = false;
            Liberty = false;
        }

        public void MoveToPlace(Roll roll)
        {
            Place += roll.Value;
            if (Place > 11)
                Place -= 12;
        }

        public void AddPoint()
        {
            Point++;
        }

        public void SetPenalty()
        {
            Penalty = true;
        }

        public void RevokeLiberty()
        {
            Liberty = false;
        }

        public void GiveLiberty()
        {
            Liberty = true;
        }
    }
}