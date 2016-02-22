using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public void changeScene (string scene) 
	{
		Application.LoadLevel (scene);

	}
}
