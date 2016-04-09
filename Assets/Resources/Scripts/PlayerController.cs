using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private bool hasGrounded;
	private int dir;
	private bool isJumping;
	private bool trigger;
	private bool canDestroy;
	public bool controls;
	private bool canFinish;
	private float jumpHeight;
	private float speed;
	private float moveX;
	private float moveX_PS4;
	private float wallJumpLR;
	private int placeholder;
	private bool isMoving;
	public bool canShoot;
	public bool canPick;
	private GameObject[] prefabs;
	private Sprite alcSprite;
	private bool scrollCanSpawn;	
	public bool canHit;

	[SerializeField]
    private GameObject gravel;

	[SerializeField]
	private GameObject breakedRock;
	[SerializeField]
	private GameObject scrollSpawn;
	[SerializeField]
	private GameObject scroll;

	Animator anim;

	private Rigidbody2D rb;

	void Start () {
		scrollCanSpawn = true;
		isJumping = false;
		canFinish = false;
		rb = GetComponent<Rigidbody2D> ();
		speed = 6;
		jumpHeight = 12;
		canDestroy = false;
		controls = true;
		canShoot = false;
		prefabs = Resources.LoadAll ("Prefabs") as GameObject[];
		anim = GetComponent<Animator> ();
		alcSprite = Resources.Load ("Sprites/Alcapao", typeof(Sprite)) as Sprite;
		canHit = false;
	}

	void canPress()
	{
		canHit = true;
	}

	void Shooting() {
        Vector2 dir = transform.GetChild(2).gameObject.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        gravel.transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        gravel.transform.position = transform.position;

        Instantiate(gravel);

		transform.GetChild(2).gameObject.transform.position = transform.position;
		transform.GetChild(2).gameObject.SetActive(false);

        controls = true;
	}

	void FixedUpdate () {

		moveX = Input.GetAxis("Horizontal");
		anim.SetBool ("isMoving", isMoving);
		Flip (rb.velocity.x);
		canFinish = true;

		if (placeholder >= 2) {

			GameObject.FindGameObjectWithTag("Alcapao").GetComponent<SpriteRenderer>().sprite = alcSprite;
			GameObject.FindGameObjectWithTag("Alcapao").name = "AlcapaoOpen";
		}

		if (controls) {
			if (Mathf.Abs (moveX * speed) > 0)
				isMoving = true;
			else
				isMoving = false;

			if (Input.GetJoystickNames ().Length > 1) {
				rb.velocity = new Vector2 (Mathf.Round (moveX) * speed, rb.velocity.y);
			} else {
				rb.velocity = new Vector2 (moveX * speed, rb.velocity.y);
			}
		

			if ((Input.GetKey (KeyCode.W) || Input.GetButton ("PS4_X")) && !isJumping) {
				rb.velocity = new Vector2 (0, jumpHeight);
				isJumping = true;
			}
			
			if ((Input.GetButtonDown ("PS4_Quad") || Input.GetKeyDown(KeyCode.Space)) && !canPick && !canShoot && canDestroy) {
				GameObject rock = GameObject.FindGameObjectWithTag ("Rock");
				Destroy (rock);
				Instantiate (breakedRock, rock.transform.position, Quaternion.identity);
				canDestroy = false;

			}

			if ((Input.GetButtonDown ("PS4_Quad") || Input.GetKeyDown(KeyCode.Space)) && canPick && !canShoot) {
				transform.GetChild (2).gameObject.SetActive (true);
				controls = false;
				isMoving = false;
				canShoot = true;
				canFinish = false;
			}
		}
		if ((Input.GetButtonDown ("PS4_Quad") || Input.GetKeyDown(KeyCode.Space)) && canShoot && canPick && canFinish) {
			Shooting();
			canShoot = false;
		}
}

	void Clean() {
		placeholder++;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Floor") {
			isJumping = false;
		}

	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Rock") {
			canDestroy = true;
		}
		if (col.gameObject.tag == "breakedRock") {
			canPick = true;

		}
		if (col.gameObject.name == "AlcapaoOpen") {
			Application.LoadLevel((Application.loadedLevel >= 6) ? 0 : Application.loadedLevel + 1);
			
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "breakedRock") {
			canPick = false;
		}
		if (col.gameObject.tag == "Rock") {
			canDestroy = false;
		}
	}

	void Flip(float dir) {
		if (dir < 0)
			transform.rotation = new Quaternion (0, 180, 0, 0);
		else if (dir > 0)
			transform.rotation = new Quaternion (0, 0, 0, 0);
	}
}
