using UnityEngine;
using System.Collections;

public class AchievementGet : MonoBehaviour {

	Vector2 targetPosition1;
	Vector2 targetPosition2;
	int gambiarra;

	void Start () {
		targetPosition1 = new Vector2 (this.transform.localPosition.x, 33f);
		targetPosition2 = new Vector2 (this.transform.localPosition.x, -170f);
		gambiarra = 0;
	}

	void Update () {
		if (this.transform.localPosition.y != targetPosition1.y && gambiarra == 0) {
			this.transform.localPosition = Vector2.MoveTowards (this.transform.localPosition, targetPosition1, 400 * Time.deltaTime);
		} else if (gambiarra < 500) {
			gambiarra++;
		} else {
			this.transform.localPosition = Vector2.MoveTowards (this.transform.localPosition, targetPosition2, 400 * Time.deltaTime);
		}
	}
}
