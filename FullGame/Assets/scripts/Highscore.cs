using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Shows the highscore on screen.
/// </summary>
public class Highscore : MonoBehaviour {
	// Update is called once per frame
	void Update() {
		GetComponent<Text>().text = PlayerPrefs.GetInt("highscore") + " Highscore";
	}
}
