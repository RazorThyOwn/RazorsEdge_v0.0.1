#pragma strict

function Start () {

	/*
	Game Functions as follows:

	Scenes:

		menu_initScreen
		Menu screen, should be the FIRST screen that is seen on loading the game. Has 3 buttons to travel between the map screen, play screen, and exit.

		menu_mapScreen
		Map screen, aka the screen that is used when generating the map. Important: Creates the WorldTerrainHolder to pass into menu_gameScreen

		menu_playerScreen
		Player Screen, very important for adding players to the game
		***TODO: Set this shit up so that way it correctly adds player to the PlayerHolder object



	Variety of Scenes contain specific objects with scripts pertinent to that scene only

	For world generation, an object called WorldTerrainHolder holds the terrain of the map.
		WorldTerrainHolder: Is initialized inside of the menu_mapScreen scene, and is set to not destroy on scene load. Should follow the game into menu_worldGeneration scene.
	For containing players, an object called PlayerHolder holds an array of the players.
		PlayerHolder: Is initialized inside of the menu_playerScreen scene, set to not destory on load.

*/

}
