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
	public GameObject[] tilesToUse;

	// # Private variables # //

	GameObject[] players;
	int numPlayers = 1;

	bool isMen = true;
	bool isMap = false;
	bool isPlayGen = false;
	bool isGame = false;

	int mapSizeDimX = 0, mapSizeDimY = 0;

	GameObject currentWorld;

	float mapSize = 1;

	MWorld mw;

	int[,] tileIDArray;
	int chunkDim = 19;

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

			Rect sliderMapSize = new Rect (540, 150, 100, 30);
			Rect sliderChunkSize = new Rect (540, 200, 100, 30);
			mapSize = Mathf.RoundToInt(GUI.HorizontalSlider(sliderMapSize,mapSize,1.0f,5.0f));
			chunkDim = Mathf.RoundToInt(GUI.HorizontalSlider(sliderChunkSize,chunkDim,19.0f,49.0f));

			string mapSizeString = "";

			if (mapSize == 1) {mapSizeString = "Extra Small (17x17)";}
			else if (mapSize == 2) {mapSizeString = "Small (25x25)";}
			else if (mapSize == 3) {mapSizeString = "Medium (31x31)";}
			else if (mapSize == 4) {mapSizeString = "Large (37x37)";}
			else if (mapSize == 5) {mapSizeString = "Extra Large (45x45)";}

			GUI.Label (new Rect (540, 160, 100, 180), mapSizeString);
			GUI.Label (new Rect(540, 210, 100, 180), ("Chunk size: "+chunkDim));

			if (GUI.Button (new Rect ((scrW / 2 - 50), 50, 100, 40), "New World")) {

				World ws = (World)currentWorld.GetComponent<World> ();

				if (ws != null) {
					killCurrentWorld ();
				}
					
				if (mapSize == 1) {mapSizeDimX = 17; mapSizeDimY = 17;}
				if (mapSize == 2) {mapSizeDimX = 25; mapSizeDimY = 25;}
				if (mapSize == 3) {mapSizeDimX = 31; mapSizeDimY = 31;}
				if (mapSize == 4) {mapSizeDimX = 37; mapSizeDimY = 37;}
				if (mapSize == 5) {mapSizeDimX = 45; mapSizeDimY = 45;}

				ws.genIslands (mapSizeDimX,mapSizeDimY);
				ws.spawnMapWorld ();
			}

			if (GUI.Button (new Rect ((scrW / 2 + 90), 50, 100, 40), "Accept World")) {

				Debug.Log ("Accepting world...");

				if (currentWorld != null) {
					
					isMap = false;
					isPlayGen = true;

					mapMen.SetActive (false);


					World worldMap = (World)currentWorld.GetComponent<World> ();
					worldMap.hideMapWorld ();

					createMWorld (mapSizeDimX, mapSizeDimY, worldMap);
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

	public void createMWorld(int dimX, int dimY, World worldMap) {

		Debug.Log(System.DateTime.Now);

		Debug.Log ("Creating MWorld...");

		tileIDArray = worldMap.getTileIDArray ();

		//Debug.Log (tileIDArray [7, 7]);

		mw = (MWorld)MWorld.CreateInstance ("MWorld");
		mw.tilesToUse = tilesToUse;
		mw.tileIDArray = tileIDArray;
		mw.initMWorld (mapSizeDimX, mapSizeDimY, chunkDim);
		mw.initChunks ((int)(mapSizeDimX/2) - 1, (int)(mapSizeDimY/2) - 1, (int)(mapSizeDimX/2) + 1, (int)(mapSizeDimY/2) + 1);


		Debug.Log(System.DateTime.Now);
		Debug.Log ("Spawning chunks");

		mw.spawnCornucopia ();

		Debug.Log(System.DateTime.Now);
	}
		
}