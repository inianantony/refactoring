namespace UglyTrivia
{
    public class Player
    {
        public string PlayerName { get; }
        public int Place { get; private set; }

        public Player(string playerName)
        {
            PlayerName = playerName;
            Point = 0;
            Penalty = false;
        }

        public bool IsWinner => Point == 6;

        public void MoveToPlace(int roll)
        {
            Place += roll;
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

        public bool Penalty { get; private set; }
        public int Point { get; private set; }
    }
}