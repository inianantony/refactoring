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
                _popQuestions.AddLast(FormatQuestion("Pop", i));
                _scienceQuestions.AddLast(FormatQuestion("Science", i));
                _sportsQuestions.AddLast(FormatQuestion("Sports", i));
                _rockQuestions.AddLast(FormatQuestion("Rock", i));
            }
        }

        private static string FormatQuestion(string type, int i)
        {
            return type + " Question " + i;
        }

        public void AskQuestion(int place)
        {
            var currentCategory = CurrentCategory(place);
            var questionList = GetQuestionList(currentCategory);

            Console.WriteLine(questionList.First());
            questionList.RemoveFirst();
        }

        private LinkedList<string> GetQuestionList(string currentCategory)
        {
            LinkedList<string> section = _rockQuestions;
            switch (currentCategory)
            {
                case "Pop":
                    section = _popQuestions;
                    break;
                case "Science":
                    section = _scienceQuestions;
                    break;
                case "Sports":
                    section = _sportsQuestions;
                    break;
                case "Rock":
                    section = _rockQuestions;
                    break;
            }

            return section;
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