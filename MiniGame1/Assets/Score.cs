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
	public static int score;
	private Text text;

	void Start()
	{
		text = GetComponent<Text>();
		score = PlayerPrefs.GetInt("score");
		highscore = PlayerPrefs.GetInt("highscore");
	}

	void Update()
	{
		// update highest score
		if (score > highscore)
		{
			highscore = score;
			PlayerPrefs.SetInt("highscore", highscore);
		}
		// show score on screen
		text.text = "Score: " + score;
	}

	public void addLaundryScore(){
		score += 100; //picking a dress is 100 points
		PlayerPrefs.SetInt ("score", score); 
	}

	public void addCakesScore(){
		score += 1; //picking a cake is 1 point
		PlayerPrefs.SetInt ("score", score); 
	}

	public void addTrollScore(){
		score += 200; //hitting the troll is 200 points
		PlayerPrefs.SetInt ("score", score); 
	}

}
