Feature: PlayingAGame
	Simulating the playing of game by 1 player or multiplayer

@SinglePlayer
Scenario: There is only 1 player in the game and he is answering corret for 6 times
	Given Player "Chet" have joined the game
	And Rolled 2 during the game play
	When Player answers the question correctly
	And Repeat rolling 2 and answering correctly for 5 more times
	Then The last correct answer should return "false"

@SinglePlayer
Scenario: There is only 1 player in the game and he answers wrong for 1 time, so he need to make an odd roll and answer correcctly and then answer 5 times to win with odd rolling
	Given Player "Chet" have joined the game
	And Rolled 2 during the game play
	When Player answers the question wrongly
	And Rolled 3 during the game play
	And Player answers the question correctly
	And Repeat rolling 3 and answering correctly for 5 more times
	Then The last correct answer should return "false"

@SinglePlayer
Scenario: There is only 1 player in the game and he answers wrong for 1 time, he made an even roll and answer correcctly and then answer 5 times with odd rolling then he will not win
	Given Player "Chet" have joined the game
	And Rolled 2 during the game play
	When Player answers the question wrongly
	And Rolled 2 during the game play
	And Player answers the question correctly
	And Repeat rolling 3 and answering correctly for 5 more times
	Then The last correct answer should return "true"

@TwoPlayers
Scenario: There are 2 players in the game and so there should be a total of 11 answer attempts and 1st player should answer 6 times to win as each player take turns to answer
	Given Player "Chet" have joined the game
	And Player "Anna" have joined the game
	And Rolled 2 during the game play
	When Player answers the question correctly
	And Repeat rolling 2 and answering correctly for 10 more times
	Then The last correct answer should return "false"

@TwoPlayers
Scenario: There are 2 players in the game and player 1 answers wrong for the 1st time, so he need to make odd roll for next 6 times for the 1st player to win as each player take turns to answer
	Given Player "Chet" have joined the game
	And Player "Anna" have joined the game
	And Rolled 2 during the game play
	When Player answers the question wrongly
	And Rolled 2 during the game play
	When Player answers the question wrongly
	And Repeat rolling 3 and answering correctly for 11 more times
	Then The last correct answer should return "false"

	@TwoPlayers
Scenario: There are 2 players in the game and player 1 answers wrong for the 1st time, and if he didn' maket odd roll for next 6 times then he cant win
	Given Player "Chet" have joined the game
	And Player "Anna" have joined the game
	And Rolled 2 during the game play
	When Player answers the question wrongly
	And Rolled 2 during the game play
	When Player answers the question wrongly
	And Rolled 2 during the game play
	When Player answers the question correctly
	And Repeat rolling 3 and answering correctly for 10 more times
	Then The last correct answer should return "true"