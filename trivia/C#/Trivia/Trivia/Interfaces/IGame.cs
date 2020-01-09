using Trivia.Models;

namespace Trivia.Interfaces
{
    public interface IGame
    {
        bool Add(Player player);
        void Roll(Roll roll);
        void AnswerCorrectly();
        void MoveToNextPlayer();
        bool HasCurrentPlayerWon();
        void AnswerWrongly();
    }
}