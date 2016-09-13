using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	MainMenuInterface mmInterface;

	void Awake(){
		mmInterface = GetComponentInParent<MainMenuInterface> ();
	}

	public void MouseUp(){
		mmInterface.StartGame ();
	}
}
