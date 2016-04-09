using UnityEngine;
using System.Collections;

public class AimController : MonoBehaviour {

	private float moveX_PS4;
	private float moveY_PS4;
	private float sensitivy;
	[SerializeField]
	private GameObject rockBullet;
	[SerializeField]
	private GameObject player;
	private bool PcanShoot;
	private bool PcanPick;

	private Vector2 aimPos;

	void Start () {
		sensitivy = 0.1f;
		PcanShoot = this.GetComponentInParent<PlayerController> ().canShoot;
		PcanPick = player.GetComponent<PlayerController>().canPick;
	}
	void FixedUpdate () {
		moveX_PS4 = Input.GetAxis ("Horizontal");
		moveY_PS4 = Input.GetAxis ("Vertical");
		if (Input.GetJoystickNames().Length > 1) {
			transform.position =
				new Vector2(Mathf.Round(moveX_PS4) * sensitivy + transform.position.x,
				            Mathf.Round(moveY_PS4) * sensitivy + transform.position.y);
		}

        else
        {
            transform.position =
                new Vector2(moveX_PS4 * sensitivy + transform.position.x,
                            moveY_PS4 * sensitivy + transform.position.y);
        }
	}
}