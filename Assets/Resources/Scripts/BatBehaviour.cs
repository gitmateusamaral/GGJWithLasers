using UnityEngine;
using System.Collections;

public class BatBehaviour : MonoBehaviour {

	private bool isFlying;
	private int speed;
	private float maxHeight;
	private float minHeight;

	void Start () {
		isFlying = false;
		speed = 1;
		maxHeight = Random.Range (4,9);
		minHeight = Random.Range (0, 3);
	}

	void goFly()
	{
		isFlying = true;
	}

	void Update () {
		if (isFlying) {
			transform.position = new Vector2 (transform.position.x + 4* Time.deltaTime, transform.position.y + speed * 5*Time.deltaTime);
			if (transform.position.y >= maxHeight) {
				speed = -1;
			} else if (transform.position.y <= minHeight) {
				speed = 1;
			}
		}
	}
}
