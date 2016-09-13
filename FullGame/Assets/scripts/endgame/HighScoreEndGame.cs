using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreEndGame : MonoBehaviour, EndGame {
	public void CreateHighScore() {
		SetupPlayerScore();
	}

	private void SetupPlayerScore() {
		GameObject highscore = GameObject.FindGameObjectWithTag(Constants.SCORE);
		highscore.GetComponent<Text>().fontSize = 22;
		RectTransform rectTrans = highscore.GetComponent<RectTransform>();
		rectTrans.anchorMin = new Vector2(.5f, 1);
		rectTrans.anchorMax = new Vector2(.5f, 1);
		rectTrans.anchoredPosition = new Vector3(0, rectTrans.anchoredPosition.y - 25);
	}
}
