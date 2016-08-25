using UnityEngine;
using System.Collections;

public class menumap_script : MonoBehaviour {

	// Instance Fields
	// Hold a World_TerrainHolder script
	// Hold rectangles for GUI positions
	// Holds world generation information

	// This script is assigned to the menu_mapScreen scene on the MenuManager_Map object

	// ROLE

	// GUI Elements:
	// Chunk Size Slider, should go from 19 to 49
	// Num Chunks Slider, should go from 11 to 41
	// Map Terrain Type Buttons, various buttons for map
	// Accept Map, should continue to next scene keeping the current map
	// Generate Map, should generate map using worldGen
	// Exit Map, return to menu_initScreen

	// On GenerateMap button press, should initialize

	public GameObject terrainObject;

	private World_TerrainHolder terrainHolder;

	private Rect accept, generate, exit, chunkSlider, numChunkSlider;

	private short chunkSize, numChunks, terrainType;

	public void Start() {

		// Assigning the script of terrainObject to terrainHolder
		terrainHolder = (World_TerrainHolder)terrainObject.GetComponent<World_TerrainHolder>();

		// Initializing the rectangles for the screen buttons
		accept = new Rect (460, 500, 150, 50);
		generate = new Rect (460, 560, 150, 50);
		exit = new Rect (460, 620, 150, 50);

		// Initializing the rectangles for sliders

		chunkSlider = new Rect (620, 500, 150, 10);
		numChunkSlider = new Rect (620, 520, 150, 10);

		// Initializing slider values
		chunkSize = 19;
		numChunks = 11;
		terrainType = 0;
	}

	public void OnGUI() {

		// Should complete all GUI Element assignments

		if (GUI.Button (accept, "Accept Map")) {
			if (terrainHolder.isValid) {
				acceptWorld ();
			} else {
				Debug.Log ("No valid world initialized!");
			}
		}

		if (GUI.Button (generate, "Generate Map")) {
			generateMap ();
		}

		if (GUI.Button (exit, "Exit Map")) {
			Application.LoadLevel ("menu_initScreen");
		}

		chunkSize = (short)GUI.HorizontalSlider (chunkSlider, chunkSize, 9f, 29f);
		numChunks = (short)GUI.HorizontalSlider (numChunkSlider, numChunks, 5f, 15f);
	}

	private void generateMap() {

		// Initializing the script object that will contain information regarding terrain
		terrainHolder = new World_TerrainHolder ();

		// Setting data and generating world
		terrainHolder.setData (chunkSize, numChunks, terrainType);
		terrainHolder.generateWorld ();

	}

	private void acceptWorld() {
		loadScene ("menu_playerScreen");
	}

	private int loadScene(string sceneName) {

		// Should take in the parameter sceneName and load that corresponding scenere
		Application.LoadLevel(sceneName);
		return 0;
	}
}
