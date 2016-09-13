using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	MainMenuInterface mmInterface;
	void Awake(){
		mmInterface = gameObject.GetComponentInParent <MainMenuInterface> ();
	}
	public void MouseUp(){
		print ("did smth");
		mmInterface.StartGame ();
	}

}
