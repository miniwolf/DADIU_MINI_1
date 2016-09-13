using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour,MainMenuInterface {

	GameObject blockingClickOnPause;
	bool shouldShowMenu = true;
	int shouldShowMenuHolder = 1;
	GameObject introStory;
	GameObject tutorial;
	GameObject settingsHolder;

	void Awake(){
		introStory = GameObject.FindGameObjectWithTag ("IntroStory");
		settingsHolder = GameObject.FindGameObjectWithTag ("SettingsScreen");
		tutorial = GameObject.FindGameObjectWithTag ("Tutorial");

		if (PlayerPrefs.GetInt ("ShouldShowIntroStory") == 1) {
			HideIntroStory();
		} else {
			ShowIntroStory();
		}
		if (PlayerPrefs.GetInt ("ShouldShowTutorial") == 1) {
			HideTutorial();
		} else {
			ShowTutorial();
		}

		blockingClickOnPause = GameObject.FindGameObjectWithTag (Constants.BLOCKINGCLICKS);
		if (PlayerPrefs.GetInt("shouldShowMenu") == 1) {
			shouldShowMenu = true;
		} else {
			shouldShowMenu = false;
		}
		if (shouldShowMenu) {
			Time.timeScale = 0;
			ShowMenu ();
		} else {
			HideSettings ();
			HideMenu ();
			StartGame ();
		}
	}

	public void ShowIntroStory(){
		introStory.GetComponent<Image>().enabled = true;
		foreach(Transform go in introStory.GetComponentInChildren<Transform>()){
			go.GetComponent<Image> ().enabled = true;
		}
		introStory.GetComponent<Image> ();

		PlayerPrefs.SetInt ("ShouldShowIntroStory", 1);
	}


	public void HideIntroStory(){
		foreach(Transform go in introStory.GetComponentInChildren<Transform>()){
			go.GetComponent<Image> ().enabled = false;
		}
		introStory.GetComponent<Image>().enabled = false;
		introStory.GetComponent<IntroStory> ().ResetImage ();
	}
	public void ShowTutorial (){ 
		tutorial.GetComponent<Image> ().enabled = true;
		tutorial.GetComponent<TutorialImages> ().ResetImage ();
	}
	public void HideTutorial(){
		tutorial.GetComponent<Image> ().enabled = false;
		tutorial.GetComponent<TutorialImages> ().ResetImage ();
	}

	public void StartGame(){
		HideMenu();
		blockingClickOnPause.SetActive (false);
		Time.timeScale = 1;
		PlayerPrefs.SetInt("ShouldShowTutorial", 1);
		PlayerPrefs.SetInt("ShouldShowIntroStory", 1);
	}

	public void ShowSettings(){
		HideMenu ();
		foreach(Transform go in settingsHolder.GetComponentInChildren<Transform>()){
			go.GetComponent<Image> ().enabled = true;
		}

	}

	public void HideSettings(){
		foreach(Transform go in settingsHolder.GetComponentInChildren<Transform>()){
			go.GetComponent<Image> ().enabled = false;
		}
	}

	public void ShowMenu(){
		HideSettings ();
		foreach(Transform go in gameObject.GetComponentInChildren<Transform>()){
			go.GetComponent<Image> ().enabled = true;
		}
		blockingClickOnPause.SetActive (true);
		PlayerPrefs.SetInt ("shouldShowMenu", 1);
		Time.timeScale = 0;
	}

	public void HideMenu(){
		foreach(Transform go in gameObject.GetComponentInChildren<Transform>()){
			go.GetComponent<Image> ().enabled = false;
		}
	}

}
