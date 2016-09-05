using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private GameObject playerHolder;
	private PlayerHolder ph;
	private PlayerInfo pi;

	private GameObject terrainHolderObj;
	private World_TerrainHolder terrainHolder;
	private World_ChunkSpawner wcs = new World_ChunkSpawner();

	private short[,] terrainChunkMap;
	private short[,] terrainMap;
	private short[,] terrainCostMap;

	private bool[,] isChunkLoad;

	private int turnCount = 0;
	private GameObject currentPlayer;

	private float xBound, yBound;
	private int chunkSize;

	public float tileSize;
	public GameObject[] tilesToUse;
	public Camera cam;

	void Start() {

		// Setting up stuff

		terrainHolderObj = GameObject.Find ("WorldTerrainHolder");
		terrainHolder = (World_TerrainHolder)terrainHolderObj.GetComponent<World_TerrainHolder>();

		playerHolder = GameObject.Find ("PlayerHolder");
		ph = (PlayerHolder)playerHolder.GetComponent<PlayerHolder> ();

		terrainChunkMap = terrainHolder.getChunkTerrain ();
		terrainCostMap = terrainHolder.getTerrainCost ();
		reloadTerrain ();
		wcs.initialize (terrainChunkMap, tilesToUse, terrainHolder.getChunkSize());
		chunkSize = terrainHolder.getChunkSize ();

		isChunkLoad = new bool[terrainChunkMap.GetLength (0), terrainChunkMap.GetLength (1)];

		// Calculating XBound

		xBound = chunkSize * terrainChunkMap.GetLength (0) * tileSize;
		xBound = xBound / 2.0f;
			
		yBound = chunkSize * terrainChunkMap.GetLength (1) * tileSize;
		yBound = yBound/2.0f;

		Debug.Log ("Calculated XBound to :" + xBound + ", YBound to :" + yBound);

		// Generating cornucopia chunks...

		short centerChunk = (short)(terrainChunkMap.GetLength (0) / 2);

		for (int i = centerChunk-2; i < centerChunk+3; i++) {
			for (int j = centerChunk-2; j < centerChunk+3; j++) {

				short[,] tmpChunk = wcs.spawnChunk ((short)i, (short)j);
				terrainHolder.copyInChunk (i,j,tmpChunk);

			}
		}

		// Spawning in cornucopia chunks

		for (int i = centerChunk - 2; i < centerChunk + 3; i++) {
			for (int j = centerChunk - 2; j < centerChunk + 3; j++) {

				short[,] tmpChunk = terrainHolder.copyOutChunk(i, j);
				loadInChunk (tmpChunk, i, j);
			}
		}

		reloadTerrain ();







		// Instantiating in players

		int numPlayers = ph.getNumPlayers ();
		float rads = 360 / numPlayers;
		rads = (rads * 3.14159f / 180.0f);

		for (int i = 0; i < numPlayers; i++) {

			float currentAngle = rads * i;

			//Debug.Log ("Angle : " + (180.0f*currentAngle/3.14159f));

			float xCord = (Mathf.Cos (currentAngle));
			float yCord = (Mathf.Sin (currentAngle));

			//Debug.Log ("X:" + xCord + ", Y:" + yCord);

			xCord *= (8.0f * 0.4f);
			yCord *= (8.0f * 0.4f);

			//Debug.Log ("X:" + xCord + ", Y:" + yCord);

			int xRemaind = (int)(xCord / 0.4f);
			int yRemaind = (int)(yCord / 0.4f);

			xCord = xRemaind * 0.4f;
			yCord = yRemaind * 0.4f;

			GameObject player = ph.getPlayer (i);
			player.SetActive (true);

			Debug.Log ("Putting player " + i + " at " + xCord + "," + yCord);
			player.transform.Translate (xCord,yCord,0);
		}

		currentPlayer = ph.getPlayer (0);
		pi = (PlayerInfo)currentPlayer.GetComponent<PlayerInfo> ();
		pi.isCurrentPlayer = true;

		UpdateCamera ();
	}

	public void UpdateCamera() {

		if (cam.transform.position.x != currentPlayer.transform.position.x || cam.transform.position.y != currentPlayer.transform.position.y) {
			
			cam.transform.Translate (new Vector3 (-cam.transform.position.x + currentPlayer.transform.position.x, -cam.transform.position.y + currentPlayer.transform.position.y));
		}

		// Checking if outside chunks.. because that is bad AS FUCKKKK!!!
		float currentX = currentPlayer.transform.position.x + xBound;
		float currentY = currentPlayer.transform.position.y + yBound;

		int chunkX = (int)(currentX / chunkSize);
		int chunkY = (int)(currentY / chunkSize);

		if (!isChunkLoad [chunkX, chunkY]) {
			Debug.Log ("outside current map... FIXME");
		}

		currentX /= tileSize;
		currentY /= tileSize;
	}

	void advanceTurn() {

		Debug.Log ("Advancing turn...");

		turnCount++;
		int currentPlayerN = turnCount % ph.getNumPlayers ();
		Debug.Log ("Setting player to " + currentPlayerN);

		currentPlayer = ph.getPlayer (currentPlayerN);

		pi.isCurrentPlayer = false;
		pi = (PlayerInfo)currentPlayer.GetComponent<PlayerInfo> ();
		pi.isCurrentPlayer = true;

		UpdateCamera ();
	}

	void loadInChunk(short[,] chunk, int chunkX, int chunkY) {

		for (int i = 0; i < chunk.GetLength (0); i++) {
			for (int j = 0; j < chunk.GetLength (1); j++) {

				short chunkVal = chunk [i, j];
				GameObject tmpTile = Instantiate (tilesToUse [chunkVal]);
				tmpTile.transform.Translate (new Vector3 (-xBound, -yBound));

				float tilesX = (chunkX * chunkSize);
				float tilesY = (chunkY * chunkSize);
				tilesX += i;
				tilesY += j;

				tilesX *= 0.4f;
				tilesY *= 0.4f;

				tmpTile.transform.Translate (new Vector3 (tilesX, tilesY));

			}
		}

		isChunkLoad [chunkX, chunkY] = true;
	}

	void reloadTerrain() {

		terrainMap = terrainHolder.getTerrain ();
	}

	void OnGUI() {

		Rect endTurn = new Rect (50, 50, 200, 50);
		if (GUI.Button (endTurn, "End turn")) {
			advanceTurn ();
		}
	}
}
