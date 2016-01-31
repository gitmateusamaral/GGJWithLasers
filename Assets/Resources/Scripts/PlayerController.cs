using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private bool hasGrounded;
	private int dir;
	private bool isJumping;
	private bool wallJump;
	private bool trigger;
	private bool canDestroy;
	public static bool controls;
	private float jumpHeight;
	private float speed;
	private float moveX;
	private float moveX_PS4;
	private float wallJumpLR;
	private bool isMoving;
	private bool canShoot;
	private GameObject[] prefabs;

	[SerializeField]
	private GameObject breakedRock;

	Animator anim;

	private Rigidbody2D rb;
	//private RaycastHit2D groundCheck;

	void Start () {
		isJumping = false;
		rb = GetComponent<Rigidbody2D> ();
		speed = 6;
		jumpHeight = 12;
		wallJump = false;
		canDestroy = false;
		controls = true;
		prefabs = Resources.LoadAll ("Prefabs") as GameObject[];
		anim = GetComponent<Animator> ();
	}

	void WallJumping(string direction)
	{
		wallJump = true;
		if (direction.Equals ("Right")) {
			wallJumpLR = -1;
		} 
		else {
			wallJumpLR = 1;
		}
	}

	void Shooting()
	{
		prefabs [0].transform.position = transform.position;
		prefabs [0].transform.LookAt (transform.GetChild(2).gameObject.transform.position);
		transform.GetChild(2).gameObject.transform.position = transform.position;
		transform.GetChild(2).gameObject.SetActive(false);
		controls = true;


	}

	void FixedUpdate () {

		moveX = Input.GetAxis("Horizontal");
		anim.SetBool ("isMoving", isMoving);
		Flip (rb.velocity.x);
		if (controls) {
			if (Mathf.Abs (moveX * speed) > 0)
				isMoving = true;
			else
				isMoving = false;

			if (Input.GetJoystickNames ().Length > 0) {
				rb.velocity = new Vector2 (Mathf.Round (moveX) * speed, rb.velocity.y);
			} 
			else {
				rb.velocity = new Vector2 (Mathf.Round (moveX) * speed, rb.velocity.y);
			}
		

			if ((Input.GetKey (KeyCode.Space) || Input.GetButton ("PS4_X")) && !isJumping) {
				rb.velocity = new Vector2 (0, jumpHeight);
				isJumping = true;
			} 
			else if ((Input.GetKey (KeyCode.Space) || Input.GetButton ("PS4_X")) && wallJump) {
				rb.velocity = new Vector2 (35 * wallJumpLR, jumpHeight);
				isJumping = true;
				wallJump = false;
			}

			if (Input.GetButton ("PS4_Quad") && canShoot) {
				transform.GetChild (2).gameObject.SetActive (true);
				controls = false;
			}

			if (Input.GetButton ("PS4_Bol")) {
				if (canDestroy) {
					GameObject rock = GameObject.FindGameObjectWithTag ("Rock");
					Instantiate (breakedRock, rock.transform.position, Quaternion.identity);
					Destroy (rock);
					canDestroy = false;
				}

				if (trigger) {
					trigger = false;
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Floor") {
			isJumping = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Lever") {
			trigger = true;
		}
		if (col.gameObject.tag == "Rock") {
			canDestroy = true;
		}
		if (col.gameObject.tag == "breakedRock") {
			canShoot = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "breakedRock") {
			canShoot = false;
		}
	}

	void Flip(float dir) {
		if (dir < 0)
			transform.rotation = new Quaternion (0, 180, 0, 0);
		else if (dir > 0)
			transform.rotation = new Quaternion (0, 0, 0, 0);
	}
}
