using UnityEngine;
using System.Collections;

public class Return : MonoBehaviour {

	void Update()
	{
		if (Input.anyKeyDown) 
		{
			Application.LoadLevel(0);
		}
	}
}
