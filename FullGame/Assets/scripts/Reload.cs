using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour {

	public void ReloadLevel() {
		PlayerPrefs.SetInt("shouldShowMenu", 0);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ReloadOnDeath() {
		PlayerPrefs.SetInt("shouldShowMenu", 1);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
