using UnityEngine;
using System.Collections;

public class menuworldgen_script : MonoBehaviour {

	// Instance Fields
	// Should contain a World_TerrainHolder, however should grab it from the hierarchy on awake

	// This script is assigned to the menu_worldGen scene on the MenuManager_WorldGen object

	// ROLE

	// Functions as a loading screen, should initialize world gen for the assigned World_TerrainHolder

	private GameObject terrainHolderObj;
	private World_TerrainHolder terrainHolder;

	void Start() {

		terrainHolderObj = GameObject.Find ("WorldTerrainHolder");
		terrainHolder = (World_TerrainHolder)terrainHolderObj.GetComponent<World_TerrainHolder>();

		terrainHolder.worldGenWorld ();
	}

	void OnGUI() {

		for (int i = 0; i < terrainHolder.getChunkTerrain ().GetLength(0); i++) {
			for (int j = 0; j < terrainHolder.getChunkTerrain ().GetLength (1); j++) {

				short number = terrainHolder.getChunkTerrain () [i, j];
				if (number == 0) {
					GUI.color = Color.blue;

				}
				if (number == 1) {
					GUI.color = Color.cyan;

				}

				if (number == 2) {
					GUI.color = Color.yellow;

				}

				if (number == 3) {
					GUI.color = Color.green;

				}

				if (number == 4) {
					GUI.color = Color.gray;
				}

					GUI.Label (new Rect (i * 20, j * 20, 20, 20), ""+number);
			}
		}
	}
}
