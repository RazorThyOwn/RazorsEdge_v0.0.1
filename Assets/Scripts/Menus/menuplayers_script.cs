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
	private int red, green, blue;
	private Rect addPlayer, nameRect;
	private Rect redSlide, blueSlide, greenSlide;

	void Start() {

		// Initializing rectangles
		addPlayer = new Rect (460, 500, 150, 50);
		nameRect = new Rect (460, 560, 150, 50);

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
			
			playerHolder.addPlayer (red, green, blue, playerName);
		}

		playerName = GUI.TextField (nameRect, playerName, 25);

		red = (int)GUI.HorizontalSlider (redSlide, red, 0, 255);
		green = (int)GUI.HorizontalSlider (blueSlide, green, 0, 255);
		blue = (int)GUI.HorizontalSlider (greenSlide, blue, 0, 255);

		GUI.color = new Color (((float)red/255f), ((float)green/255f), ((float)blue/255f));
		GUI.Label (new Rect (620, 560, 100, 200), "This is the player color");
	}

}
