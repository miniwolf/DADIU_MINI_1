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
	private ScoreInterface score;

	void Start() {
		text = GetComponentInChildren<Text>();
		score = GameObject.FindGameObjectWithTag(Constants.SCORE).GetComponent<ScoreInterface>();

		// number of initial cakes is set to 0
		SetCakes();
	}

	void Update() {
		text.text = "" + PlayerPrefs.GetInt("numCakes");
	}

	private void SetCakes() {
		PlayerPrefs.SetInt("numCakes", numCakes);
	}

	public void AddCake() {
		numCakes++;
		SetCakes();
		score.AddCakesScore();
	}

	public void RemoveCake() {
		if ( numCakes > 0 ) {
			numCakes--;
			SetCakes();
		}
	}

	public int GetNumCakes() {
		return numCakes;
	}
}
