using UnityEngine;
using System.Collections;

public class menuplayers_script : MonoBehaviour {

	// Instance Fields
	// Rectangles for the gui buttons

	// This script is assigned to the menu_playerScreen scene on the MenuManager_Players object

	// ROLE

	// Should create buttons to add players and then continue to the game
	// Mostly GUI Stuff

	public GameObject playerHolderObject;
	private PlayerHolder playerHolder;

	private string playerName;

	private static int maxPlayers = 16;

	private int red, green, blue;
	private Rect addPlayer, nameRect, continueRect;
	private Rect redSlide, blueSlide, greenSlide;

	void Start() {

		// Initializing rectangles
		addPlayer = new Rect (460, 500, 150, 50);
		nameRect = new Rect (460, 560, 150, 50);
		continueRect = new Rect (460, 620, 150, 50);

		redSlide = new Rect (620, 500, 150, 10);
		blueSlide = new Rect (620, 520, 150, 10);
		greenSlide = new Rect (620, 540, 150, 10);

		playerName = "Player Name";
		red = 255;
		green = 255;
		blue = 255;

		playerHolder = (PlayerHolder)playerHolderObject.GetComponent<PlayerHolder> ();
	}

	void OnGUI() {

		// Draw all GUI elements for the scene
		if (GUI.Button (addPlayer, "Add Player")) {

			if (playerHolder.getNumPlayers() < maxPlayers) {
				Debug.Log ("Adding player");
				playerHolder.addPlayer (red, green, blue, playerName);
			}
		}

		playerName = GUI.TextField (nameRect, playerName, 25);

		red = (int)GUI.HorizontalSlider (redSlide, red, 0, 255);
		green = (int)GUI.HorizontalSlider (blueSlide, green, 0, 255);
		blue = (int)GUI.HorizontalSlider (greenSlide, blue, 0, 255);

		GUI.color = new Color (((float)red/255f), ((float)green/255f), ((float)blue/255f));
		GUI.Label (new Rect (620, 560, 100, 200), "This is the player color");

		for (int i = 0; i < playerHolder.getNumPlayers(); i++) {

			PlayerInfo pi = (PlayerInfo)playerHolder.getPlayer (i).GetComponent<PlayerInfo> ();
			string name = pi.name;

			GUI.color = new Color (((float)pi.red/255f), ((float)pi.green/255f), ((float)pi.blue/255f));
			GUI.Label (new Rect (465, 470 - 30 * i, 190, 30), ("Position "+(i+1)+", "+name));

			if (GUI.Button (new Rect (630, 470 - 30 * i, 20, 20), "X")) {
				playerHolder.removePlayer (i);
			}
		}

		if (GUI.Button (continueRect, "Continue to Game!")) {
			loadScene ("menu_worldgenScreen");
		}
	}

	private int loadScene(string sceneName) {

		// Should take in the parameter sceneName and load that corresponding scenere
		Application.LoadLevel(sceneName);
		return 0;
	}
}
