using UnityEngine;
using System.Collections;

public class MWorld_Tile : ScriptableObject {

	public int walkCost;
	public int tileID;

	public void initTile(int _walkCost, int _tileID) {
		walkCost = _walkCost;
		tileID = _tileID;
	}
}
