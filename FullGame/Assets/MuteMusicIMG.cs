using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteMusicIMG : MonoBehaviour {
	bool music = true;
	public Sprite[] imgs = new Sprite[2];

	void Start(){
		gameObject.GetComponent<Image> ().sprite = imgs [1];
	}

	public void ToggleMusic(){
		if (!music) {
			gameObject.GetComponent<Image> ().sprite = imgs [1];
			music = true;
            AkSoundEngine.PostEvent("forestSoundscape", GameObject.FindGameObjectWithTag(Constants.SOUND));
        }
        else{
			gameObject.GetComponent<Image> ().sprite = imgs [0];
			music = false;
            AkSoundEngine.StopAll();

        }
		print ("Toggled Music");
	}
}
