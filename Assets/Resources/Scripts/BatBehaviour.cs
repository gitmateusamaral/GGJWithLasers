using UnityEngine;
using System.Collections;

public class BatBehaviour : MonoBehaviour {

	private bool isFlying;
	private int speed;
	private float maxHeight;
	private float minHeight;
	private Animator animatorBat;
	private AnimatorStateInfo animStateBat;

	void Start () {
		animatorBat = this.GetComponent<Animator>();
		isFlying = false;
		speed = 1;
		maxHeight = Random.Range (5,9);
		minHeight = Random.Range (1, 2);
	}

	void goFly()
	{
		isFlying = true;
		animatorBat.Play("BatAnim");
	}

	void Update () {

		if (this.transform.position.x >= 15f) {
			Destroy(gameObject);
		}

		if (isFlying) {
			if (transform.position.y >= maxHeight) {
				speed = -1;
			} else if (transform.position.y <= minHeight) {
				speed = 1;
			}
			transform.position = new Vector2 (transform.position.x + 4* Time.deltaTime, Mathf.Clamp((transform.position.y + speed * 5*Time.deltaTime),minHeight,maxHeight));
		}
	}
}
