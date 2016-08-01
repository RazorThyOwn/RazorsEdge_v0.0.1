using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public void navTo(GameObject wt) {
		this.transform.Translate (new Vector3 (wt.transform.position.x, wt.transform.position.y, -1));
	}
}
