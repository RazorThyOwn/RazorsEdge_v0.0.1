using UnityEngine;
using System.Collections;

public class World_Generator : MonoBehaviour {

	// Instance Fields
	// Should contain an Int regarding number of chunks, an Int regarding chunk size, and a Short determining terrain generation type

	// 0 = checkerboard

	private static short DEEP_OCEAN = 0;
	private static short SHALLOW_OCEAN = 1;
	private static short SAND = 2;
	private static short GRASS = 3;
	private static short FOREST = 4;

	private int numChunks, chunkSize;
	private short terrainType;

	private short[,] terrain;
	private short[,] terrainCost;

	private int sizeX, sizeY;

	public World_Generator(int _numChunks, int _chunkSize, short _terrainType, short[,] _terrain, short[,] _terrainCost) {
		
		numChunks = _numChunks;
		chunkSize = _chunkSize;
		terrainType = _terrainType;
		terrain = _terrain;
		terrainCost = _terrainCost;

		sizeX = chunkSize * numChunks;
		sizeY = sizeX;

	}

	public void generate() {

		Debug.Log ("Generating...");

		if (terrainType == 0) {
			islands (15, 5);
		}
	}





















	public void generateCheckerboard() {

		Debug.Log ("Generating checkerboard...");

		for (int i = 0; i < sizeX; i++) {
			for (int j = 0; j < sizeY; j++) {

				if (i % 2 == 1) {
					if (j % 2 == 1) {
						terrain [i, j] = 0;
						terrainCost [i, j] = 1;
					} else {
						terrain [i, j] = 1;
						terrainCost [i, j] = 2;
					}
				} else {
					if (j % 2 == 1) {
						terrain [i, j] = 1;
						terrainCost [i, j] = 2;
					} else {
						terrain [i, j] = 0;
						terrainCost [i, j] = 1;
					}
				}
			}
		}

		Debug.Log ("Generated checkerboard...");
		Debug.Log ((terrain [0,0] + "," + terrain [0,1] + "," + terrain [1,0] + "," + terrain [1,1]));
	}

	public void islands(int numIslands, int depthFactor) {

		Debug.Log ("Generating island...");

		fillSolidWorld(DEEP_OCEAN);

		// Generating the island
		int count = 0;

		while (count < numIslands) {

			int xToUse = Random.Range (sizeX / depthFactor, sizeX - sizeX / depthFactor);
			int yToUse = Random.Range (sizeY / depthFactor, sizeY - sizeY / depthFactor);
			int length = Random.Range (sizeX / (2 * depthFactor), sizeX / depthFactor);
			int height = Random.Range (sizeX / (2 * depthFactor), sizeY / depthFactor);

			genEllipse (GRASS, xToUse, yToUse, length, height);

			count++;

		}

		// Creating sand and shallow water

		genLip (DEEP_OCEAN, SHALLOW_OCEAN, GRASS);
		genLip (GRASS, SAND, SHALLOW_OCEAN);

		int[] lineFunc = { 0, 0, 1 };

		// Creating forest

		count = 0;
		while (count < numIslands/2) {

			int xToUse = Random.Range (sizeX / depthFactor, sizeX - sizeX / depthFactor);
			int yToUse = Random.Range (sizeY / depthFactor, sizeY - sizeY / depthFactor);
			int length = Random.Range (sizeX / (2 * depthFactor), sizeX / depthFactor);
			int height = Random.Range (sizeX / (2 * depthFactor), sizeY / depthFactor);

			replaceOnEllipse (FOREST, xToUse, yToUse, length, height, GRASS);

			count++;

		}
		Debug.Log ("Islands generated... hopefully");
	}

	public void fillSolidWorld(short tileToUse) {

		Debug.Log ("Filling solid world with " + tileToUse+".....");

		for (int i = 0; i < sizeX; i++) {
			for (int j = 0; j < sizeY; j++) {

				terrain [i, j] = tileToUse;
			}
		}

		Debug.Log ("Filled solid world with " + tileToUse);
	}

	public void genLip(short tarTile, short replaceTile, short neighborTile) {

		Debug.Log ("Lip Generating on "+tarTile+" against "+neighborTile+" replaced with "+replaceTile);

		for (int x = 0; x < sizeX; x++) {
			for (int y = 0; y < sizeY; y++) {

				Debug.Log ("X:" + x + ", Y:" + y);
				short tile = terrain [x, y];

				if (tile == tarTile) {

					if (y - 1 >= 0 && neighborTile == terrain[x, y - 1]) {
						Debug.Log ("Replacing... Y-1");
						terrain [x, y] = replaceTile;
					} else if (x - 1 >= 0 && neighborTile == terrain[x - 1, y]) {
						Debug.Log ("Replacing... X-1");

						terrain [x, y] = replaceTile;
					} else if (x + 1 < sizeX && neighborTile == terrain[x + 1, y]) {
						Debug.Log ("Replacing... X+1");

						terrain [x, y] = replaceTile;
					} else if (y + 1 < sizeY && neighborTile == terrain[x, y + 1]) {
						Debug.Log ("Replacing... Y+1");

						terrain [x, y] = replaceTile;
					}

				}
			}
		}

		Debug.Log ("Lip Generated");
	}

	public void genEllipse(short replaceTile, int tarX, int tarY, int length, int height) {

		Debug.Log ("Generating ellipse at x:"+tarX+", y:"+tarY+" of length:"+length+" and height:"+height+", using "+replaceTile);

		for (int x = tarX - length; x <= tarX + length; x++) {
			for (int y = tarY - height; y <= tarY + height; y++) {


				float theta = Mathf.Atan2 ((y - tarY), (x - tarX));
				float radius = Mathf.Sqrt (Mathf.Pow (height / 2, 2) * Mathf.Pow (Mathf.Cos (theta), 2) + Mathf.Pow (length / 2, 2) * Mathf.Pow (Mathf.Sin (theta), 2));
				float distance = getDistance (tarX, tarY, x, y);

				if (distance <= radius) {

					if (x >= 0 && x < sizeX && y >= 0 && y < sizeY) {

						short curTile = terrain [x, y];
						if (curTile!=replaceTile) {

							terrain [x, y] = replaceTile;
						} else {
							//Debug.Log ("Was equal, not replacing");
						}
					}
				}
			}
		}

		Debug.Log ("Ellipse generated...");
	}

	public void replaceOnEllipse(short replaceTile, int tarX, int tarY, int length, int height, short targetTile) {

		Debug.Log ("Replacing ellipse at x:"+tarX+", y:"+tarY+" of length:"+length+" and height:"+height+", using "+replaceTile);

		for (int x = tarX - length; x <= tarX + length; x++) {
			for (int y = tarY - height; y <= tarY + height; y++) {


				float theta = Mathf.Atan2 ((y - tarY), (x - tarX));
				float radius = Mathf.Sqrt (Mathf.Pow (height / 2, 2) * Mathf.Pow (Mathf.Cos (theta), 2) + Mathf.Pow (length / 2, 2) * Mathf.Pow (Mathf.Sin (theta), 2));
				float distance = getDistance (tarX, tarY, x, y);

				if (distance <= radius) {

					if (x >= 0 && x < sizeX && y >= 0 && y < sizeY) {

						short curTile = terrain [x, y];
						if (curTile == targetTile) {

							terrain [x, y] = replaceTile;
						} else {
							//Debug.Log ("Was equal, not replacing");
						}
					}
				}
			}
		}

		Debug.Log ("Ellipse replaced...");
	}

	public float getDistance(int x1, int y1, int x2, int y2) {

		float distance = Mathf.Sqrt( Mathf.Pow(x2 - x1,2) + Mathf.Pow(y2 - y1,2));
		return distance;
	}
















	public short[,] getTerrain() {

		return terrain;

	}

	public short[,] getTerrainCost() {

		return terrainCost;

	}
}
