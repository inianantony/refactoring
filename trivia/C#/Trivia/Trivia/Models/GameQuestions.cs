using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia.Models
{
    public class GameQuestions
    {
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        public GameQuestions()
        {
            PrepareQuestions();
        }

        public void PrepareQuestions()
        {
            for (int i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public void AskQuestion(int place)
        {
            var currentCategory = CurrentCategory(place);
            if (currentCategory == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (currentCategory == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (currentCategory == "Sports")
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (currentCategory == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        public string CurrentCategory(int place)
        {
            if (place == 0) return "Pop";
            if (place == 4) return "Pop";
            if (place == 8) return "Pop";
            if (place == 1) return "Science";
            if (place == 5) return "Science";
            if (place == 9) return "Science";
            if (place == 2) return "Sports";
            if (place == 6) return "Sports";
            if (place == 10) return "Sports";
            return "Rock";
        }
    }
}