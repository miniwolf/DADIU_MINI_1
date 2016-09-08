using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	private int timeLeft;
	private int startingTime;

	// Use this for initialization
	void Start () {
		startingTime = 30;
		timeLeft = startingTime;
	}

	// Update is called once per frame
	void FixedUpdate () {
		timeLeft = startingTime - (int)Time.timeSinceLevelLoad;
		GetComponent<Text> ().text = "" + timeLeft;

		if ( timeLeft == 0 ) {
			// load new level/screen
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
