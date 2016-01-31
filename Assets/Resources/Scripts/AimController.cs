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

	private Vector2 aimPos;

	void Start () {
		sensitivy = 0.1f;
	}
	void FixedUpdate () {
		moveX_PS4 = Input.GetAxis ("Horizontal");
		moveY_PS4 = Input.GetAxis ("Vertical");
		if (Input.GetJoystickNames().Length > 0) {
			transform.position =
				new Vector2(Mathf.Round(moveX_PS4) * sensitivy + transform.position.x,
				            Mathf.Round(moveY_PS4) * sensitivy + transform.position.y);
		}

		if (Input.GetButton ("PS4_Tri")) {
			this.gameObject.SetActive (false);
			Vector3 _aimPos = this.transform.TransformDirection (Vector3.forward);
			aimPos = new Vector2 (_aimPos.x, _aimPos.y);
			GameObject bullet = Instantiate (rockBullet, player.transform.position, Quaternion.identity) as GameObject;
			float step = 6 * Time.deltaTime;
			bullet.transform.position = Vector2.MoveTowards (bullet.transform.position, this.transform.position, step);
		}
	}
}