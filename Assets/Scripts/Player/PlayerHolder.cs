using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHolder : MonoBehaviour {

	// Instance Fields
	// Should contain a 1D array of player objects, initialized on scene load and then filled with players throug the GUI
	// int listing the number of players in the game
	// Should also contain all the information regarding the player

	public GameObject playerObject;
	public static int numPlayers = 0;
	private static List<GameObject> playerList;

	void Awake() {
		playerList = new List<GameObject> ();
		DontDestroyOnLoad (this);
	}

	public void addPlayer(int red, int green, int blue, string name) {

		numPlayers++;
		GameObject newPlayer = Instantiate (playerObject);
		newPlayer.SetActive (false);
		PlayerInfo pl = (PlayerInfo)newPlayer.GetComponent<PlayerInfo> ();

		pl.red = red;
		pl.green = green;
		pl.blue = blue;
		pl.name = name;

		newPlayer.name = name;

		pl.setColor ();
		Debug.Log ("Added player " + name + " of color " + red + "," + green + "," + blue);


		playerList.Add (newPlayer);
	}

	public int getNumPlayers() {
		return numPlayers;
	}

	public void removePlayer(int index) {
		numPlayers--;
		playerList.RemoveAt (index);
	}

	public GameObject getPlayer(int index) {
		return playerList [index];
	}
}
