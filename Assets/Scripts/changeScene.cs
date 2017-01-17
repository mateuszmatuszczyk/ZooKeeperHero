using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {
	//barry is fat lolololololololololol

	// Update is called once per frame
	public void changeToScene (string scenetoChangeTo) {
		Application.LoadLevel(scenetoChangeTo);
	}
}
