using UnityEngine;
using System.Collections;

public class MWorld_Chunk : ScriptableObject {

	// Ideals for MWorld_Chunk

	// Should store an array of MWorld_Tiles within the class
	// Easy access to each tile at any given point
	// Quick load times!

	private MWorld_Tile[,] tiles;
	private int chunkDimX;
	private int chunkDimY;

	public void initMWorld_Chunk(int _chunkDimX, int _chunkDimY) {

		Debug.Log ("Creating chunk...");
		tiles = new MWorld_Tile[_chunkDimX, _chunkDimY];
		chunkDimX = _chunkDimX;
		chunkDimY = _chunkDimY;
	}

	public int initTiles() {

		if (tiles == null) {
			return 0;
		}

		for (int i = 0; i < chunkDimX; i++) {
			for (int j = 0; j < chunkDimY; j++) {
				Debug.Log ("Init tiles " + i + "," + j);
				MWorld_Tile mwt = (MWorld_Tile)MWorld_Tile.CreateInstance ("MWorld_Tile");
				tiles [i, j] = mwt;
			}
		}

		return 1;
	}
}
