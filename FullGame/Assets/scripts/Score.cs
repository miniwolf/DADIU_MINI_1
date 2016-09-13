using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Shows score on screen and updates the highest score.
/// Implements the ScoreInterface methods for adding score
/// to the player.
/// </summary>
public class Score : MonoBehaviour, ScoreInterface {
	private static int highscore;
	private Text text;
	private int score;
	public int extraTime; //extra time when picking laundry
	private Timer timer;

	void Start() {
		text = GetComponent<Text>();
		extraTime = 2;
		// player score set to 0 and highscore taken from the preferences
		score = 0;
		PlayerPrefs.SetInt("score", 0);
		highscore = PlayerPrefs.GetInt("highscore");
		timer = GameObject.FindGameObjectWithTag(Constants.TIMETEXT).GetComponent<Timer>();
	}

	void Update() {
		// update highest score
		if ( score > highscore ) {
			highscore = score;
			PlayerPrefs.SetInt("highscore", highscore);
		}
		// show score on screen
		text.text = score + " Points";
	}

	public void AddLaundryScore() {
		score += 100; //picking a dress is 100 points
		PlayerPrefs.SetInt ("score", score);
		// add extra time when picking laundry
		timer.AddExtraTime(extraTime);
	}

	public void AddCakesScore() {
		score += 1; //picking a cake is 1 point
		PlayerPrefs.SetInt ("score", score);
	}

	public void AddTrollScore() {
		score += 200; //hitting the troll is 200 points
		PlayerPrefs.SetInt ("score", score);
	}
}
