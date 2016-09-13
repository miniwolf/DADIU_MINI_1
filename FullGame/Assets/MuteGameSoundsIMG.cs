using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteGameSoundsIMG : MonoBehaviour {
	bool gameSounds = true;
	public Sprite[] imgs = new Sprite[2];
	void Start(){
		gameObject.GetComponent<Image> ().sprite = imgs [1];;
	}
	public void ToggleGameSounds(){
		if (!gameSounds) {
			gameObject.GetComponent<Image> ().sprite = imgs [1];
			gameSounds = true;
		} else {
			gameObject.GetComponent<Image> ().sprite = imgs [0];
			gameSounds = false;
		}
		print("Toggled GameSounds");
	}
}
