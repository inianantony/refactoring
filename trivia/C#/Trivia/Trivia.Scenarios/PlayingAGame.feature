Feature: PlayingAGame
	Simulating the playing of game by 1 player or multiplayer

@mytag
Scenario: There is only 1 player in the game and he is answering corret for 6 times
	Given Player "Chet" have joined the game
	And Rolled 2 during the game play
	When Player answers the question correctly
	And Repeat rolling 2 and answering correctly for 5 more times
	Then The last correct answer should return false
