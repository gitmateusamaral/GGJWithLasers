using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	private int levelSelected;
	private float ps4X;

	[SerializeField]
	private Button play;

	[SerializeField]
	private Button credits;

	void Update () {

		ps4X = Input.GetAxis ("Horizontal");

		if(ps4X > 0) {
			levelSelected = 1;
			if(credits.GetComponent<RectTransform>().localScale.y < 1.2f)
			{
			credits.GetComponent<RectTransform>().localScale =
				new Vector3(credits.GetComponent<RectTransform>().localScale.x + 0.2f,
				            credits.GetComponent<RectTransform>().localScale.y + 0.2f,
				            credits.GetComponent<RectTransform>().localScale.z + 0.2f);
			play.GetComponent<RectTransform>().localScale =
				new Vector3(play.GetComponent<RectTransform>().localScale.x - 0.2f,
				            play.GetComponent<RectTransform>().localScale.y - 0.2f,
				            play.GetComponent<RectTransform>().localScale.z - 0.2f);
			}
		}
		if (ps4X < 0) {
			levelSelected = 2;
			if(play.GetComponent<RectTransform>().localScale.y < 1.2f)
			{
			play.GetComponent<RectTransform>().localScale =
				new Vector3(play.GetComponent<RectTransform>().localScale.x + 0.2f,
				            play.GetComponent<RectTransform>().localScale.y + 0.2f,
				            play.GetComponent<RectTransform>().localScale.z + 0.2f);
			credits.GetComponent<RectTransform>().localScale =
				new Vector3(credits.GetComponent<RectTransform>().localScale.x - 0.2f,
				            credits.GetComponent<RectTransform>().localScale.y - 0.2f,
				            credits.GetComponent<RectTransform>().localScale.z - 0.2f);
			}
			}

		if (Input.GetButtonDown ("PS4_X")) {
			Application.LoadLevel(levelSelected);
		}
	}
}
