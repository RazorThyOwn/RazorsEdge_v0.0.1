using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Nav : MonoBehaviour {
	
	Grid grid;

	void Awake() {
		
		grid = GetComponent<Grid> ();

	}

	public void FindPath(Vector2 startPos, Vector2 targetPos) {
		
		Stopwatch sw = new Stopwatch ();
		sw.Start ();
		//Create Nodes from the Vector2s given
		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);
		//Create the list of open and closed nodes
		Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
		HashSet<Node> closedSet = new HashSet<Node> ();
		//Add the beginning node to the open set
		openSet.Add (startNode);

		while (openSet.Count > 0) {
			Node node = openSet.RemoveFirst();

			/*
			Node node = openSet [0];

			//find the node with the lowest fCost and that is nearest towards destination
			for (int i = 1; i < openSet.Count; i++) {
				if(openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost){
					if (openSet [i].hCost < node.hCost) {
						node = openSet [i];
					}
				}
			}

			//Add lowest fCost to closedSet and remoce is from openSet
			openSet.Remove (node); */
			closedSet.Add (node);

			//if we have reached destination then end the while loop
			if (node == targetNode) {
				sw.Stop ();
				print ("Path found: " + sw.ElapsedMilliseconds + "ms");
				RetracePath (startNode, targetNode);
				return;
			}

			//loop through each of the neighbors of the currentNode
			foreach (Node neighbour in grid.GetNeighbours(node)) {
				if (!neighbour.walkable || closedSet.Contains (neighbour)) {
					continue;
				}
				//check to see if new path is shorter or neighbour is not in open then replace the current node with the shorter node
				int newMovementCostToNeighbour = node.gCost + GetDistance (node, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance (neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains (neighbour))
						openSet.Add (neighbour);
					else
						openSet.UpdateItem (neighbour);
  
				}
			}
		}
	}

	//find the path through the parent nodes
	void RetracePath(Node startNode, Node endNode){
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;
		//put the parent nodes into a list tracing backwards
		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}

		path.Reverse ();

		grid.path = path;
		PlayerMove.playerPath = path;

	}

	int GetDistance(Node nodeA, Node nodeB){
		//find the distance length of axis of the makshift grid of nodeA and nodeB
		int dstX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

		int costMult = 10 * nodeB.getCost();

		if (dstX > dstY) {
			return costMult * dstY + costMult * (dstX - dstY);
		}
		return costMult * dstX + costMult * (dstY - dstX);
	}

	public Vector2 getMouseNode(){

		Node mouse = grid.NodeFromWorldPoint ((Vector2)Input.mousePosition);
		Vector2 mouseNodePos = mouse.worldPosition;
		return mouseNodePos;

	}

}
