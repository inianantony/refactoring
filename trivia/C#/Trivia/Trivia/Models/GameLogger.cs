using System;
using Trivia.Services;

namespace Trivia.Models
{
    public class GameLogger
    {
        public void LogPlayerAddition(GamePlayers gamePlayers, string playerName)
        {
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + gamePlayers.PlayerCount);
        }

        public void LogGamePoint(GamePlayers gamePlayers)
        {
            Console.WriteLine($"{gamePlayers.CurrentPlayerName} now has {gamePlayers.CurrentPlayerPoints} Gold Coins.");
        }

        public void LogSettingPenalty(GamePlayers gamePlayers)
        {
            Console.WriteLine($"{gamePlayers.CurrentPlayerName} was sent to the penalty box");
        }
    }
}