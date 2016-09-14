using UnityEngine;
using System.Collections;

public class Reload : MonoBehaviour {
	bool hasBeenPressed;
	void Start(){
		hasBeenPressed = false;
	}
	public void ReloadLevel() {
		if (!hasBeenPressed) {
			PlayerPrefs.SetInt ("shouldShowMenu", 0);
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
