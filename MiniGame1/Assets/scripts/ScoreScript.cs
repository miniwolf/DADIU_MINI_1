using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	private static int highscore;
	public static int score;
	private Text text;

	void Awake()
	{
		text = GetComponent<Text>();
		score = 0;
		highscore = PlayerPrefs.GetInt("highscore");
	}

	void Update()
	{
		if (score > highscore)
		{
			highscore = score;
			PlayerPrefs.SetInt("highscore", highscore);
		}
		text.text = "Score: " + score;
	}


	void IncrementScore(){
		score++;
		PlayerPrefs.SetInt ("score", score); 
	}

	void DecrementScore(){
		score--;
		PlayerPrefs.SetInt ("score", score);
	}
}
