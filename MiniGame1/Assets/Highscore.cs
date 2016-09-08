using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Shows the highscore on screen.
/// </summary>
public class Highscore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text>().text = "Highscore: " + PlayerPrefs.GetInt("highscore");
	}
}
