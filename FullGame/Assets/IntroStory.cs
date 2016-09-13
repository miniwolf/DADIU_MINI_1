using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroStory : MonoBehaviour {

	public Sprite[] imgs = new Sprite[9];
	int iterator = 0;

	void Awake(){
		GetComponent<Image> ().sprite = imgs [0];
	}


	public void NextImage(){
		if (iterator < imgs.Length-1) {
			iterator++;
			GetComponent<Image> ().sprite = imgs [iterator];
		} else {
			iterator = 0;
			gameObject.GetComponent<Image> ().enabled = false;
			foreach (Transform go in GetComponentInChildren<Transform>()) {
				go.GetComponent<Image> ().enabled = false;
			}
			ResetImage ();
		}
	}
	public void ResetImage(){
		iterator = 0;
		GetComponent<Image> ().sprite = imgs [0];
	}
}
