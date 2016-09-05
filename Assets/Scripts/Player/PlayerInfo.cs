using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

	public int red, green, blue;
	public string name;

	public bool isCurrentPlayer = false;

	public void setColor() {
		SpriteRenderer renderer = this.GetComponent<SpriteRenderer> ();
		renderer.color = new Color (red, green, blue);
	}

	void Awake() {
		DontDestroyOnLoad (this);
	}

	void Update() {

		if (Input.GetKeyDown ("w") && isCurrentPlayer) {
			this.transform.Translate (new Vector3 (0, 0.4f, 0));
			updateCam ();
		}
		if (Input.GetKeyDown ("s") && isCurrentPlayer) {
			this.transform.Translate (new Vector3 (0, -0.4f, 0));
			updateCam ();
		}
		if (Input.GetKeyDown ("a") && isCurrentPlayer) {
			this.transform.Translate (new Vector3 (-0.4f, 0, 0));
			updateCam ();
		}
		if (Input.GetKeyDown ("d") && isCurrentPlayer) {
			this.transform.Translate (new Vector3 (0.4f, 0, 0));
			updateCam ();
		}
	}

	void updateCam() {
		
		GameObject gameMan = GameObject.Find ("GameManager");
		GameManager gm = (GameManager)gameMan.GetComponent<GameManager> ();
		gm.UpdateCamera ();
	}
}
