using UnityEngine;
using System.Collections;

public class World_TerrainHolder : MonoBehaviour {

	// Instance Fields
	// NEEDS to hold an integer 2D array named terrain[,] containing the tileIDs for co-ordinates
	// Should also contain a link to the WorldGen script, in order to properly generate terrain...

	// Role

	// Holds all the Terrain information in the map

	public bool isValid = false;

	public static short [,] terrain;
	public static short [,] terrainCost;

	public static int num = 0;

	private static short chunkSize, numChunks, terrainType;

	private static int sizeX, sizeY;

	private World_Generator wg;

	void Awake() {
		DontDestroyOnLoad (this);
		terrainType = 0;
	}

	public void setData(short _chunkSize, short _numChunks, short _terrainType) {

		// Assiggns all the important information before generation
		chunkSize = _chunkSize;
		numChunks = _numChunks;
		terrainType = _terrainType;
		sizeX = chunkSize * numChunks;
		sizeY = sizeX;
	}

	public void generateWorld() {

		// All this method does is, essentially, initialize the world to contain all zeroes. Does not actually use world gen
		
		terrain = new short[sizeX, sizeY];
		terrainCost = new short[sizeX, sizeY];

		for (int i = 0; i < sizeX; i++) {
			for (int j = 0; j < sizeY; j++) {
				terrain [i, j] = 0;
				terrainCost [i, j] = 1;
			}
		}

		Debug.Log ("Initialized the world... NumChunks: "+numChunks+", ChunkSize: "+chunkSize);
		isValid = true;
	}


	public void worldGenWorld() {


		// Actually generates the world

		wg = new World_Generator (chunkSize, numChunks, terrainType, terrain, terrainCost);
		wg.generate ();

		terrain = wg.getTerrain ();
		terrainCost = wg.getTerrainCost ();

	}

	public short[,] getTerrain() {
		return terrain;
	}

	public short[,] getTerrainCost() {
		return terrainCost;
	}
}
