using UnityEngine;
using System.Collections;

public class Reload : MonoBehaviour {
	public void ReloadLevel() {
		PlayerPrefs.SetInt("shouldShowMenu", 0);
		Application.LoadLevel(Application.loadedLevel);
	}
}
