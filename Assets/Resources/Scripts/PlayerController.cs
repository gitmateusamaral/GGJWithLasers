using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private bool hasGrounded;
	private int dir;
	private bool isJumping;
	private float jumpHeight;
	private float speed;
	private float moveX;
	//private Vector2 playerPos;

	private Rigidbody2D rb;
	//private RaycastHit2D groundCheck;

	void Start () {
		isJumping = false;
		rb = GetComponent<Rigidbody2D> ();
		speed = 9;
		jumpHeight = 12;
	}

	void FixedUpdate () {
		//playerPos = new Vector2 (this.transform.position.x, this.transform.position.y);
		//groundCheck = Physics2D.Raycast (playerPos, Vector2.down, 1);

		moveX = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2 (moveX * speed, rb.velocity.y);

		if (Input.GetKey (KeyCode.Space) && !isJumping) {
			rb.velocity = new Vector2 (0, jumpHeight);
			isJumping = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "Tile") {
			isJumping = false;
		}
	}
}
