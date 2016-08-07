using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour {

	public Transform seeker;
	public Vector2 target;
	bool mousePressed;

	public float speed = .01f;

	public static List<Node> playerPath = new List<Node>();

	Nav playerNavigation;

	void Start(){
		target = new Vector2 ();
		playerNavigation = GetComponent<Nav> ();
		seeker = this.transform;
		mousePressed = false;

	}

	void Update(){
		if(mousePressed == true)
			playerNavigation.FindPath ((Vector2)seeker.position, (Vector2)target);

		if (Input.GetKey (KeyCode.Mouse0)) {
			Debug.Log ("Mouse Pressed");
			target = playerNavigation.getMouseNode ();
			mousePressed = true;
			Debug.Log ("Mouse Position:" + target);
		}



	}

	void fixedUpdate(){

		if (mousePressed == true) {
			makePlayerPath ();
		}

	}

	public void makePlayerPath(){

		foreach(Node n in playerPath){

			Vector3 temp = n.worldPosition;

			if (transform.position != temp) {
				Vector2 p = Vector2.MoveTowards (transform.position, temp, speed);
				GetComponent<Rigidbody2D> ().MovePosition (p);
				break;
			}

			temp = Vector3.zero;

		}

	}

}
