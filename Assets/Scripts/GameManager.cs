using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// THIS IS THE GLOBAL CONTROLLER
	// THIS IS GOD
	// HE SEES ALL
	// KNOWS ALL
	// CONTROLS ALL


	// # Global Variables for Player and World # //

	public GameObject worldHolder;
	public GameObject playerPrefab;
	public GameObject mainMen;
	public GameObject mapMen;
	public GameObject clonePlayerObject;

	// # Private variables # //

	GameObject[] players;
	int numPlayers = 1;

	bool isMen = true;
	bool isMap = false;
	bool isPlayGen = false;
	bool isGame = false;

	GameObject currentWorld;


	// # When the game launches # //

	public void Start() {

		mainMen = Instantiate (mainMen);
		mapMen = Instantiate (mapMen);
		mainMen.SetActive (true);
		mapMen.SetActive (false);
	}
		
	public void genCharacters() {

	}

	public void OnGUI() {

		int scrW = Screen.width;
		int scrH = Screen.height;

		// Rendering the main menu

		if (isMen) {

			if (GUI.Button(new Rect( (scrW/2 - 50), 300, 100, 40), "Play Game")) {

				currentWorld = Instantiate (worldHolder);

				isMap = true;
				isMen = false;

				mainMen.SetActive (false);
				mapMen.SetActive (true);

			}
			if (GUI.Button(new Rect( (scrW/2 - 50), 350, 100, 40), "Options")) {
				Debug.Log ("Button pressed.");
			}
			if (GUI.Button(new Rect( (scrW/2 - 50), 400, 100, 40), "Quit Game")) {
				Debug.Log ("Button pressed.");
				Application.Quit ();
			}
		}

		// Rendering the map menu

		if (isMap) {

			// Input fields

			if (GUI.Button (new Rect ((scrW / 2 - 50), 50, 100, 40), "New World")) {

				World ws = (World)currentWorld.GetComponent<World> ();

				if (ws == null) {
					killCurrentWorld ();
				}
					
				ws.genIslands ();
				ws.spawnWorld ();
			}

			if (GUI.Button (new Rect ((scrW / 2 + 90), 50, 100, 40), "Accept World")) {

				if (currentWorld != null) {
					isMap = false;
					isPlayGen = true;

					mapMen.SetActive (false);
				}
			}

			if (GUI.Button (new Rect ((scrW / 2 - 190), 50, 100, 40), "Exit World Gen")) {

				World ws = (World)currentWorld.GetComponent<World> ();

				if (ws != null) {
					killCurrentWorld ();
				}

				Destroy (currentWorld);

				isMap = false;
				isMen = true;

				mapMen.SetActive (false);
				mainMen.SetActive (true);
			}
				
		}

		// Rendering the options menu

		// Rendering the game itself
	}

	public void killCurrentWorld() {

		World ws = (World)currentWorld.GetComponent<World> ();
		if (ws != null) {
			ws.killMe();
		}
	}

	public void genAllPlayers () {

		players = new GameObject[numPlayers];

		for (int i = 0; i < numPlayers; i++) {

			GameObject playerTempObj = Instantiate (clonePlayerObject);

			//PlayerSpawn.GeneratePlayer ("Bear", inputName, playerTempObj);

			players [i] = playerTempObj;
		}

	}
		
}