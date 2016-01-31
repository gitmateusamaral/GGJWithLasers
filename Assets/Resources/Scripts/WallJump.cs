using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "Tile") {
			GameObject.FindGameObjectWithTag("Player").SendMessage("WallJumping",this.gameObject.name);
		} 
	}
}
