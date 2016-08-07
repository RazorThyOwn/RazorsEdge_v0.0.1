using UnityEngine;
using System.Collections;

public class WorldGen : MonoBehaviour {

	private GameObject[] tilesToUse;

	private Grid grid;

	// Constructor

	public WorldGen(GameObject[] tilesToUse) {
		
		this.tilesToUse = tilesToUse;
	}

	///////////////////////////
	// Worldtype Generations //
	///////////////////////////







	 

	/// Checkerboard the specified dimX and dimY.
	/// Creates a checkerboard grid of size dimX and dimY using tilesToUse 0 and 1
	/// <param name="dimX">Dim x.</param>
	/// <param name="dimY">Dim y.</param>

	public GameObject[,] checkerboard(int dimX, int dimY) {

		GameObject[,] world = new GameObject[dimX, dimY];

		for (int i = 0; i < dimX; i++) {
			for (int j = 0; j < dimY; j++) {

				GameObject worldTile;

				if (i % 2 == 1) {
					if (j % 2 == 1) {
						worldTile = Instantiate (tilesToUse [0]);
					} else {
						worldTile = Instantiate (tilesToUse [1]);
					}
				} else {
					if (j % 2 == 1) {
						worldTile = Instantiate (tilesToUse [1]);
					} else {
						worldTile = Instantiate (tilesToUse [0]);
					}
				}

				world 	[i, j] = worldTile;
			}
		}
		//spawnWorld (world, dimX, dimY);

		return world;
	}

	// Larger the depthFactor, the smaller the water edge is between the edge of the map. Should ALWAYS be > 2
	public GameObject[,] islands(int dimX, int dimY, int numIslands, int depthFactor) {

		Debug.Log ("Initializing world...");
		GameObject[,] world = new GameObject[dimX, dimY];
		Debug.Log (world);

		fillSolidWorld (world, tilesToUse [0], dimX, dimY);

		Debug.Log (world);

		// Generating the island
		int count = 0;

		while (count < numIslands) {
			
			int xToUse = Random.Range (dimX / depthFactor, dimX - dimX / depthFactor);
			int yToUse = Random.Range (dimY / depthFactor, dimY - dimY / depthFactor);
			int length = Random.Range (dimX / (2 * depthFactor), dimX / depthFactor);
			int height = Random.Range (dimX / (2 * depthFactor), dimY / depthFactor);

			genEllipse (world, tilesToUse[3], xToUse, yToUse, length, height);

			count++;

		}

		// Creating sand and shallow water
		world = genLip(world, tilesToUse[0],tilesToUse[1],tilesToUse[3]);

		world = genLip (world, tilesToUse [3], tilesToUse [2], tilesToUse [1] );

		int[] lineFunc = { 0, 0, 1 };

		// Creating mountains
		genOnPolynomial(world, lineFunc, 0, dimX, 1, tilesToUse[4], -2);

		// Creating forest

		count = 0;
		while (count < numIslands/2) {

			int xToUse = Random.Range (dimX / depthFactor, dimX - dimX / depthFactor);
			int yToUse = Random.Range (dimY / depthFactor, dimY - dimY / depthFactor);
			int length = Random.Range (dimX / (2 * depthFactor), dimX / depthFactor);
			int height = Random.Range (dimX / (2 * depthFactor), dimY / depthFactor);

			replaceOnEllipse (world, tilesToUse [5], xToUse, yToUse, length, height, tilesToUse [3]);

			count++;

		}

		Debug.Log ("Generated islands within worldgen...");
		Debug.Log (world);
		//spawnWorld (world, dimX, dimY);
		return world;

	}











	public void fillSolidWorld(GameObject[,] world, GameObject tileToUse, int dimX, int dimY) {

		for (int i = 0; i < dimX; i++) {
			for (int j = 0; j < dimY; j++) {

				GameObject worldTile = Instantiate (tilesToUse [0]);
				world[i,j] = worldTile;
			}
		}
	}



	public GameObject[,] genLip(GameObject[,] world, GameObject tarTile, GameObject replaceTile, GameObject neighborTile) {

		GameObject[,] returnWorld = world;

		Debug.Log ("Lip Generating");

		for (int x = 0; x < world.GetLength (0); x++) {
			for (int y = 0; y < world.GetLength (1); y++) {

				Debug.Log ("X:" + x + ", Y:" + y);
				GameObject tile = world [x, y];

				if (tile.CompareTag(tarTile.tag)) {

				if (y - 1 >= 0 && neighborTile.CompareTag(world[x,y-1].tag) ) {
					Debug.Log ("Replacing... Y-1");

					Destroy(world[x,y]);
					returnWorld [x, y] = Instantiate (replaceTile);
				}
				else if (x - 1 >= 0 && neighborTile.CompareTag(world[x-1,y].tag) ) {
					Debug.Log ("Replacing... X-1");

					Destroy(world[x,y]);
					returnWorld [x, y] = Instantiate (replaceTile);
				}
				else if (x + 1 < world.GetLength(0) && neighborTile.CompareTag(world[x+1,y].tag) ) {
					Debug.Log ("Replacing... X+1");

					Destroy(world[x,y]);
					returnWorld [x, y] = Instantiate (replaceTile);
				}
				else if (y + 1 < world.GetLength(1) && neighborTile.CompareTag(world[x,y+1].tag) ) {
					Debug.Log ("Replacing... Y+1");

					Destroy(world[x,y]);
					returnWorld [x, y] = Instantiate (replaceTile);
				}

				}
			}
		}

		return returnWorld;
	}

	public void replaceOnPolynomial(GameObject[,] world, int[] function, int startX, int endX, int thickness, GameObject replaceTile, GameObject tarTile) {
	
	}

	public void genOnPolynomial(GameObject[,] world, int[] function, int startX, int endX, int thickness, GameObject tile, float shift) {
		 
	}

	public void genBetweenPolynomial(int[] functionArrayTop, int[] functionArrayBottom, int startX, int endX, GameObject tile) {

	}

	public void replaceBetweenPolynomial(int[] functionArrayTop, int[] functionArrayBottom, int startX, int endX, GameObject replaceTile, GameObject tarTile) {

	}

	public void genEllipse(GameObject[,] world, GameObject tile, int tarX, int tarY, int length, int height) {

		//Debug.Log ("Generating ellipse at x:"+tarX+", y:"+tarY+" of length:"+length+" and height:"+height);

		for (int x = tarX - length; x <= tarX + length; x++) {
			for (int y = tarY - height; y <= tarY + height; y++) {


				float theta = Mathf.Atan2 ((y - tarY), (x - tarX));
				float radius = Mathf.Sqrt (Mathf.Pow (height / 2, 2) * Mathf.Pow (Mathf.Cos (theta), 2) + Mathf.Pow (length / 2, 2) * Mathf.Pow (Mathf.Sin (theta), 2));
				float distance = getDistance (tarX, tarY, x, y);

				if (distance <= radius) {

					if (x >= 0 && x < world.GetLength (0) && y >= 0 && y < world.GetLength (1)) {
						
						GameObject tarTile = world [x, y];
						if (!tarTile.GetComponent<SpriteRenderer> ().sprite.Equals (tile.GetComponent<SpriteRenderer> ().sprite)) {

							Destroy (tarTile);

							world [x, y] = Instantiate (tile);
						} else {
							//Debug.Log ("Was equal, not replacing");
						}
					}
				}
			}
		}
	}

	public void replaceOnEllipse(GameObject[,] world, GameObject tile, int tarX, int tarY, int length, int height, GameObject targetTile) {

		//Debug.Log ("Generating ellipse at x:"+tarX+", y:"+tarY+" of length:"+length+" and height:"+height);

		for (int x = tarX - length; x <= tarX + length; x++) {
			for (int y = tarY - height; y <= tarY + height; y++) {


				float theta = Mathf.Atan2 ((y - tarY), (x - tarX));
				float radius = Mathf.Sqrt (Mathf.Pow (height / 2, 2) * Mathf.Pow (Mathf.Cos (theta), 2) + Mathf.Pow (length / 2, 2) * Mathf.Pow (Mathf.Sin (theta), 2));
				float distance = getDistance (tarX, tarY, x, y);

				if (distance <= radius) {

					if (x >= 0 && x < world.GetLength (0) && y >= 0 && y < world.GetLength (1)) {

						GameObject wTile = world[x,y];
						if (wTile.CompareTag(targetTile.tag)) {
							Destroy(world[x,y]);
							world[x,y] = Instantiate(tile);
						}
					}
				}
			}
		}
	}

	public float getDistance(int x1, int y1, int x2, int y2) {
			
		float distance = Mathf.Sqrt( Mathf.Pow(x2 - x1,2) + Mathf.Pow(y2 - y1,2));
		return distance;
	}

	public Grid getGrid() {
		Grid retGrid = grid;
		Destroy (grid);
		return grid;
	}
}


/* //Debug.Log ("Generating on polynomial...");

int startY = 0;
int endY = 0;

// Generating the polynomial on the x-axis

Debug.Log ("|");
Debug.Log ("|");
Debug.Log ("|");
Debug.Log ("|");
Debug.Log	 ("Generating on x...");
Debug.Log ("|");
Debug.Log ("|");
Debug.Log ("|");
Debug.Log ("|");

for (int x = startX; x < endX; x++) {

	float funcX = 0;

	for (int i = function.Length-1; i >= 0; i--) {

		Debug.Log ("Calculating function y val at " + x);
		float funcVal = function [i];
		Debug.Log ("FuncVal: " + funcVal +" i: "+i);
		float pow = Mathf.Pow (x + shift, i);
		Debug.Log ("Pow: " + pow);
		funcX += (funcVal) * (pow);
		Debug.Log ("FuncX: " + funcX);
	}

	Debug.Log ("|");

	if (x == startX) {
		startY = (int)funcX;
		if (startY < 0) {
			startY = 0;
		}
	}
	if (x == endX - 1) {
		endY = (int)funcX;
		//Debug.Log ("Assigning endY...");
		if (endY > world.GetLength (1)) {
			//Debug.Log ("endY too big...");
			endY = world.GetLength (1);
			//Debug.Log ("Assigning endY...");
		}
	}

	funcX = Mathf.Round (funcX);

	Debug.Log ("|");
	Debug.Log ("Final FuncX: "+funcX);

	if ((int)funcX >= 0 && (int)funcX <= world.GetLength(1)) {
		Destroy(world[x ,(int)funcX]);
		world [x, (int)funcX] = Instantiate (tile);
	}
}

// Generating the polynomial on the y-axis 

Debug.Log ("|");
Debug.Log ("|");
Debug.Log ("|");
Debug.Log ("|");
Debug.Log ("Generating on y..."+startY+","+endY);
Debug.Log ("|");
Debug.Log ("|");
Debug.Log ("|");
Debug.Log ("|");

for (int funcX = startY; funcX < endY; funcX++) {

	float x = 0;

	Debug.Log ("Calculating function y val at " + funcX);

	for (int i = function.Length-1; i > 0; i--) {

		float funcVal = function [i];
		if (function [i] != 0) {
			funcVal = (1f / function [i]);
		}
		Debug.Log ("funcVal: " + funcVal +" i: "+i);
		float pow = Mathf.Pow (funcX + shift, 1f/i);
		Debug.Log ("Pow: " + pow);
		x += (funcVal) * (pow);
		Debug.Log ("xVal: " + x);
	}

	if (function [0] != 0) {
		x += (1f / function [0]);
	}
	x = Mathf.Round (x);

	Debug.Log ("Final X: " + x);

	//Debug.Log ("funcX:"+funcX+", x:"+x);

	if ((int)x >= 0 && (int)x <= world.GetLength (1)) {
		Debug.Log ("Creating tile at " + ((int)x) + "," + funcX);
		Destroy (world [(int)x, funcX]);
		world [(int)x, funcX] = Instantiate (tile);
	}
}*/