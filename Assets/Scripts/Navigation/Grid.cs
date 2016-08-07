using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {


	public Vector2 gridWorldSize;
	public float nodeRadius;
	public GameManager gameManager;

	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	//set the nodeDiameter and determine the grid size by taking the x and y of the world size and dividing it by the diameter
	public Grid(float _nodeRadius) {
		nodeRadius = _nodeRadius;
	}

	void Awake() {
		nodeDiameter = nodeRadius * 2;
	}

	void Update(){
		
		gridWorldSize = new Vector2(gameManager.mapSizeDimX*.4f, gameManager.mapSizeDimY*.4f);
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid();

	}
	public int MaxSize{
		get{return gridSizeX * gridSizeY; }
	}

	//makes a new grid with an array of nodes
	void CreateGrid(){
		grid = new Node[gridSizeX, gridSizeY];
		Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x/2 - Vector2.up * gridWorldSize.y/2;

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
				bool walkable = true;//((Physics2D.OverlapCircle (worldPoint, nodeRadius, unwalkableMask)) == null);
				grid [x, y] = new Node(walkable, worldPoint, x, y, 1);

			}
		}
	}

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node> ();

		for(int x = -1; x <= 1; x++){
			for(int y = -1; y <= 1; y++){
				//check if it is looking at center
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				//check if neighbor is inside grid
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add (grid [checkX, checkY]);
				}
			}
		}

		//return the list of neighbours
		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector2 worldPosition){
		//finds the percent the X and Y is on by taking the X given adding it to hal the grid world and dividing it by all the grid world
		float percentX = ((worldPosition.x + gridWorldSize.x / 2)) / gridWorldSize.x;
		float percentY = ((worldPosition.y + gridWorldSize.y / 2)) / gridWorldSize.y;
		/*float percentX = (worldPosition.x - transform.position.x) / gridWorldSize.x + .5f - (nodeRadius / gridWorldSize.x);
		float percentY = (worldPosition.y - transform.position.y) / gridWorldSize.y + .5f - (nodeRadius / gridWorldSize.y);*/
		//clamps or keeps the value between 0 and 1 so if we ask for a position outside of map it won't give us an error
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid [x, y];

	}

	public List<Node> path;

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
		List<Node> others = new List<Node> ();
		if(grid != null){
			foreach (Node n in grid){
				if (path != null) {
					if (path.Contains (n)) {
						Gizmos.color = Color.black;
					}
				}

				Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiameter - .1f));
			}
		}
	}

}
