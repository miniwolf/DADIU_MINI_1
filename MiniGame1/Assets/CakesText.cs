using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Shows number of cakes on screen. 
/// Implements the CakesInterface methods for 
/// adding or removing 1 cake.
/// </summary>
public class CakesText : MonoBehaviour, CakesTextInterface {

	private int numCakes;
	private Text text;
	public Score score;

	void Start (){
		text = GetComponent<Text> ();
		numCakes = PlayerPrefs.GetInt("numCakes");
	}

	void Update () {
		text.text = "" + PlayerPrefs.GetInt("numCakes");
	}

	public void addCake (){
		numCakes++;
		PlayerPrefs.SetInt ("numCakes", numCakes);
		score.addCakesScore ();
	}

	public void removeCake (){
		if (numCakes > 0) {
			numCakes--;
			PlayerPrefs.SetInt ("numCakes", numCakes);
		}
	}
}
