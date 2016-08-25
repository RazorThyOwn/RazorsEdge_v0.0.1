using UnityEngine;
using System.Collections;

public class menuinit_script : MonoBehaviour {

	// This script is assigned to the menu_initScreen scene on the MenuManager_init object

	// Instance Fields
	// Rects for each button

	// ROLE

	// Should create GUI buttons on the main menu screne
	// 1) Play button, should load corresponding play scene
	// 2) Options button, should load corresponding option scene
	// 3) Quit button, should quit game

	// TODO: **********
		// Ensure rectangle creation puts it in the write position regardless of screne size

	private Rect playRect, optionsRect, exitRect;

	public void Start() {

		// Create and assign the rectangle position and sizes for the buttons

		Debug.Log ("menuinit_script Starting: Creating rectangles");

		playRect = new Rect (460, 500, 150, 50);
		optionsRect = new Rect (460, 560, 150, 50);
		exitRect = new Rect (460, 620, 150, 50);

		Debug.Log (playRect);

	}

	void OnGUI() {

		// Initialize 3 buttons here using GUIButton

		if (GUI.Button(playRect, "Play")) {
			// Load Scene Play
			loadScene("menu_mapScreen");
		}
		
		if (GUI.Button(optionsRect, "Options")) {
			loadScene("menu_optionsScreen");
		}

		if (GUI.Button (exitRect, "Exit")) {
			Application.Quit();
		}

	}

	private int loadScene(string sceneName) {

		// Should take in the parameter sceneName and load that corresponding scenere
		Application.LoadLevel(sceneName);
		return 0;
	}
}
