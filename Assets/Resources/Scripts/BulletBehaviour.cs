using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
		
	private Sprite scrollSprite;
	public bool isOff = false;
	private Sprite pressed;
	private Sprite alcSprite;


	void Start () {
		scrollSprite = Resources.Load ("Sprites/candle_off", typeof(Sprite)) as Sprite;
		pressed = Resources.Load ("Sprites/PressurePlate-ON", typeof(Sprite)) as Sprite;
		alcSprite = Resources.Load ("Sprites/Alcapao", typeof(Sprite)) as Sprite;
	}

	void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * 10f);

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag.Equals ("Candle")) {
			if(col.name.Equals("CandleOn"))
			{
				col.GetComponent<SpriteRenderer>().sprite = scrollSprite;
				GameObject.FindGameObjectWithTag("Player").SendMessage("Clean");
				col.name = "CandleOff";
			}
			Destroy(gameObject);
		}

		if(col.name.Equals ("Bounds"))
		{
			if(GameObject.FindGameObjectsWithTag("Bat").Length > 0)
			{
				GameObject[] bats = GameObject.FindGameObjectsWithTag("Bat");
				for(int i =0; i < bats.Length;i++)
				{
					bats[i].SendMessage("goFly");
				}
				
				GameObject.FindGameObjectWithTag("Player").SendMessage("canPress");
			}
			Destroy(gameObject);
		}
		Debug.Log (col.tag);
		if (col.tag.Equals ("Pressure")) {
			if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canHit)
			{
				col.GetComponent<SpriteRenderer>().sprite = pressed;
				GameObject.FindGameObjectWithTag("Alcapao").GetComponent<SpriteRenderer>().sprite = alcSprite;
				GameObject.FindGameObjectWithTag("Alcapao").name = "AlcapaoOpen";
			}
		}
	}
}
