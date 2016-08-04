using UnityEngine;
using System.Collections;

public class MWorld : ScriptableObject {

	// Ideal requirements for MWorld

	// Ability to hold the entire gameworld as a series of 'chunks' using the MWorld_Chunk class
	// Ability to load and deload chunks quickly
	// Speed!

	private MWorld_Chunk[,] chunks;
	private int chunkSize;
	private int numChunkX, numChunkY;

	public void initMWorld(int dimX, int dimY, int _chunkSize) {
		Debug.Log ("Initiailizing MWorld...");
		chunks = new MWorld_Chunk[dimX, dimY];
		chunkSize = _chunkSize;
		Debug.Log ("Mworld Initialized!");
		numChunkX = dimX;
		numChunkY = dimY;
	}

	public int initChunks() {

		Debug.Log ("Initialzing each MWorld Chunk...");

		if (chunks==null) {return 0;}

		for (int i = 0; i < numChunkX; i++) {
			for (int j = 0; j < numChunkY; j++) {

				// Initializing each chunk
				Debug.Log("Initializing chunk "+i+","+j);
				MWorld_Chunk tmpChunk = (MWorld_Chunk)MWorld_Chunk.CreateInstance ("MWorld_Chunk");
				tmpChunk.initMWorld_Chunk (chunkSize, chunkSize);
				tmpChunk.initTiles ();

				chunks [i, j] = tmpChunk;
			}
		}

		Debug.Log ("Initialized chunks!");

		return 1;
	}
}
