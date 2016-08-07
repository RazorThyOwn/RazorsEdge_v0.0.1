using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public GameObject[] tilesToUse;
	public int dimX, dimY;

	private GameObject[,] localArray;
	private Grid grid;

	public void genIslands(int dimX_, int dimY_) {

		this.dimX = dimX_;
		this.dimY = dimY_;

		WorldGen wg = new WorldGen (tilesToUse);
		localArray = wg.islands (dimX, dimY, 25, 4);
		Debug.Log ("Generated islands...");
		Debug.Log (getTileIDArray ());
		grid = wg.getGrid ();
	}

	public void spawnMapWorld() {

		float bottomX = 2.6f - (dimX / 2) * 0.4f;
		float bottomY = 3.8f - (dimY / 2) * 0.4f;

		for (int i = 0; i < dimX; i++) {
			for (int j = 0; j < dimY; j++) {
				Debug.Log ("Spawning map tiles...");
				Debug.Log (localArray);
				GameObject worldTile = localArray [i, j];
				Debug.Log (worldTile);

				worldTile.transform.Translate (new Vector3 (0.4f*i + bottomX, 0.4f*j + bottomY, 0));
			}
		}
	}

	public void hideMapWorld() {

		for (int i = 0; i < dimX; i++) {
			for (int j = 0; j < dimY; j++) {
				GameObject worldTile = localArray [i, j];
				worldTile.SetActive (false);
			}
		}
	}

	public void showMapWorld() {
		
		for (int i = 0; i < dimX; i++) {
			for (int j = 0; j < dimY; j++) {
				GameObject worldTile = localArray [i, j];
				worldTile.SetActive (true);
			}
		}
	}
		
	public void pressedTile(float x, float y) {
		
		float i = Mathf.Floor(x / 0.4f);
		float j = Mathf.Floor(y / 0.4f);

		if ( i >= 0 && j >= 0 && i < localArray.GetLength(0) && j < localArray.GetLength(1) ) {
			GameObject pressedTile = localArray [(int)i, (int)j];
		}
	}

	public int[,] getTileIDArray() {

		Debug.Log ("Geting tileIDArray");

		int[,] returnArray = new int[localArray.GetLength (0), localArray.GetLength (1)];

		for (int i = 0; i < localArray.GetLength(0); i++) {
			for (int j = 0; j < localArray.GetLength(1); j++) {
				string tag = localArray[i,j].tag;

				int id = -1;
				if (tag.Equals("ter_deepwater")) {id = 0;}
				else if (tag.Equals("ter_water")) {id = 1;}
				else if (tag.Equals("ter_sand")) {id = 2;}
				else if (tag.Equals("ter_grass")) {id = 3;}
				else if (tag.Equals("ter_mountain")) {id = 4;}
				else if (tag.Equals("ter_forest")) {id = 5;}

				returnArray [i, j] = id;
			}
		}

		Debug.Log ("Returning tileIDArray");

		return returnArray;
	}

	public void killMe() {
		if (localArray != null) {
			for (int i = 0; i < localArray.GetLength (0); i++) {
				for (int j = 0; j < localArray.GetLength (1); j++) {
					GameObject destroyTile = localArray [i, j];
					Debug.Log ("Destroying " + destroyTile + " at " + i + "," + j);
					Destroy (destroyTile);
				}
			}
		}
	}
}
