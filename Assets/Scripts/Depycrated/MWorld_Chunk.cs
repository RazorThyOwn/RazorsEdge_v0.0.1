using UnityEngine;
using System.Collections;

public class MWorld_Chunk : ScriptableObject {

	// Ideals for MWorld_Chunk

	// Should store an array of MWorld_Tiles within the class
	// Easy access to each tile at any given point
	// Quick load times!

	public int terrainType;

	public GameObject[] tilesToUse;
	private MWorld_Tile[,] tiles;
	private int chunkDimX;
	private int chunkDimY;
	private int locX, locY;
	public int totChunksX, totChunksY;

	public void initMWorld_Chunk(int _chunkDimX, int _chunkDimY, int _locX, int _locY) {

		Debug.Log ("Creating chunk...");
		tiles = new MWorld_Tile[_chunkDimX, _chunkDimY];
		chunkDimX = _chunkDimX;
		chunkDimY = _chunkDimY;
		locX = _locX;
		locY = _locY;
	}

	public void setTerrainType(int _terrainType) {
		terrainType = _terrainType;
		Debug.Log ("Set terrain to " + terrainType);
	}

	public int initTiles() {

		if (tiles == null) {
			return 0;
		}

		for (int i = 0; i < chunkDimX; i++) {
			for (int j = 0; j < chunkDimY; j++) {
				Debug.Log ("Init tiles " + i + "," + j);
				MWorld_Tile mwt = (MWorld_Tile)MWorld_Tile.CreateInstance ("MWorld_Tile");
				mwt.initTile (1, terrainType);
				tiles [i, j] = mwt;
			}
		}

		return 1;
	}

	public void spawnChunk() {
		
		//Debug.Log (" EX :" + locX + "," + totChunksX + "," + chunkDimX);
		//Debug.Log (" EX :" + locY + "," + totChunksY + "," + chunkDimY);

		for (int i = 0; i < chunkDimX; i++) {
			for (int j = 0; j < chunkDimY; j++) {
				
				GameObject tile = Instantiate (tilesToUse[terrainType]);
				float chunkXOffset = (locX - (int)(totChunksX / 2) ) * (0.4f * chunkDimX);
				float chunkYOffset = (locY - (int)(totChunksY / 2) ) * (0.4f * chunkDimY);
				float tileXOffset = (i * 0.4f) - (int)(chunkDimX / 2) * 0.4f;
				float tileYOffset = (j * 0.4f) - (int)(chunkDimY / 2) * 0.4f;

				//Debug.Log ("Spawning tile at " + chunkXOffset + "," + tileXOffset + "," + chunkYOffset + "," + tileYOffset);

				tile.transform.Translate (new Vector3 (chunkXOffset + tileXOffset, chunkYOffset + tileYOffset));
			}
		}
	}
}
