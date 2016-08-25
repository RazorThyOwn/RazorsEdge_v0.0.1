using UnityEngine;
using System.Collections;

public class World_TerrainHolder : MonoBehaviour {

	// Instance Fields
	// NEEDS to hold an integer 2D array named terrain[,] containing the tileIDs for co-ordinates

	// Role

	// Holds all the Terrain information in the map

	private short[,] terrain;
	private short[,] terrainCost;

	private short chunkSize, numChunks, terrainType;

	private int sizeX, sizeY;

	void Awake() {
		DontDestroyOnLoad (this);
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
		
		terrain = new short[sizeX, sizeY];
		terrainCost = new short[sizeX, sizeY];

		for (int i = 0; i < sizeX; i++) {
			for (int j = 0; j < sizeY; j++) {
				terrain [i, j] = 0;
				terrainCost [i, j] = 1;
			}
		}

		Debug.Log ("Initialized the world... NumChunks: "+numChunks+", ChunkSize: "+chunkSize);
	}
}
