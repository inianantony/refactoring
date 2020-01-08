﻿using System;
using Trivia.Models;
using Trivia.Services;

namespace UglyTrivia
{
    public class Game
    {
        private readonly GamePlayers _gamePlayers;
        private readonly GameQuestions _gameQuestions;

        public Game()
        {
            _gamePlayers = new GamePlayers();
            _gameQuestions = new GameQuestions();
        }

        public bool Add(Player player)
        {
            _gamePlayers.AddPlayer(player);
            LogPlayerAddition(player);
            return true;
        }

        public void LogPlayerAddition(Player player)
        {
            Console.WriteLine(player.PlayerName + " was added");
            Console.WriteLine("They are player number " + _gamePlayers.PlayerCount);
        }

        public void Roll(Roll roll)
        {
            new RollBehaviour(_gamePlayers, _gameQuestions).MakeRollAction(roll);
        }

        public bool WasCorrectlyAnswered()
        {
            return new CorrectAnswerBehaviour(_gamePlayers).MakeCorrectAnswer();
        }

        public bool WrongAnswer()
        {
            return new WrongAnswerBehaviour(_gamePlayers).MakeWrongAnswer();
        }
    }
}
