using System;

namespace Trivia.Models
{
    public class GameLogger
    {
        public void LogPlayerAddition(GamePlayers gamePlayers, string playerName)
        {
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + gamePlayers.PlayerCount);
        }

        public void LogIntroToRolling(int roll, GamePlayers gamePlayers)
        {
            Console.WriteLine(gamePlayers.CurrentPlayerName + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
        }

        public void LogQuestionCategory(GameQuestions gameQuestions, GamePlayers gamePlayers)
        {
            Console.WriteLine("The category is " + gameQuestions.CurrentCategory(gamePlayers.CurrentPlayersPlace));
        }

        public void LogTheRolling(GamePlayers gamePlayers)
        {
            Console.WriteLine($"{gamePlayers.CurrentPlayerName}'s new location is {gamePlayers.CurrentPlayersPlace}");
        }

        public void LogGamePoint(GamePlayers gamePlayers)
        {
            Console.WriteLine($"{gamePlayers.CurrentPlayerName} now has {gamePlayers.CurrentPlayerPoints} Gold Coins.");
        }

        public void LogSettingPenalty(GamePlayers gamePlayers)
        {
            Console.WriteLine($"{gamePlayers.CurrentPlayerName} was sent to the penalty box");
        }

        public string LogGrantOrRevokeLiberty(string msgToggler, GamePlayers gamePlayers)
        {
            var msg = $"{gamePlayers.CurrentPlayerName} is{msgToggler} getting out of the penalty box";
            return msg;
        }
    }
}