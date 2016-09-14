using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeLanguage : MonoBehaviour {
	public void ChangeLang(int currScene){
		if (currScene == 1) {
			//Change to English
			SceneManager.LoadScene (0);
		}else if(currScene == 0){
			//Change to Danish
			SceneManager.LoadScene (1);			
		}



	}
}
