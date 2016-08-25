using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHolder : MonoBehaviour {

	// Instance Fields
	// Should contain a 1D array of player objects, initialized on scene load and then filled with players throug the GUI
	// int listing the number of players in the game
	// Should also contain all the information regarding the player

	public GameObject playerObject;
	public int numPlayers;
	private List<GameObject> playerList;

	void Awake() {
		DontDestroyOnLoad (this);
	}

	public void addPlayer(int red, int green, int blue, string name) {

		GameObject newPlayer = Instantiate (playerObject);
		PlayerInfo pl = (PlayerInfo)newPlayer.GetComponent<PlayerInfo> ();

		pl.red = red;
		pl.green = green;
		pl.blue = blue;
		pl.name = name;

		Debug.Log ("Added player " + name + " of color " + red + "," + green + "," + blue);
	}
}
