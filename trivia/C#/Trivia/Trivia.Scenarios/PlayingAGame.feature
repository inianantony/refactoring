Feature: PlayingAGame
	Simulating the playing of game by 1 player or multiplayer

@live
Scenario: There is only 1 player in the game and he is answering corret for 6 times
	Given Player "Chet" have joined the game
	And Rolled 2 during the game play
	When Player answers the question correctly
	And Repeat rolling 2 and answering correctly for 5 more times
	Then The last correct answer should return "false"

@live
Scenario: There is only 1 player in the game and he answers wrong for 1 time, so he need to make an odd roll and answer correcctly and then answer 5 times to win with odd rolling
	Given Player "Chet" have joined the game
	And Rolled 2 during the game play
	When Player answers the question wrongly
	And Rolled 3 during the game play
	And Player answers the question correctly
	And Repeat rolling 3 and answering correctly for 5 more times
	Then The last correct answer should return "false"

@live
Scenario: There is only 1 player in the game and he answers wrong for 1 time, he made an even roll and answer correcctly and then answer 5 times with odd rolling then he will not win
	Given Player "Chet" have joined the game
	And Rolled 2 during the game play
	When Player answers the question wrongly
	And Rolled 2 during the game play
	And Player answers the question correctly
	And Repeat rolling 3 and answering correctly for 5 more times
	Then The last correct answer should return "true"