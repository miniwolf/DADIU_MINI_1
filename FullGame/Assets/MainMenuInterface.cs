using UnityEngine;
using System.Collections;


public interface MainMenuInterface {
	void HideIntroStory();
	void ShowIntroStory();
	void ShowTutorial();
	void HideTutorial();
	void StartGame();
	void ShowSettings();
	void HideSettings();
	void ShowMenu();
	void HideMenu();
}