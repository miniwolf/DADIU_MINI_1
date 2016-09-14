using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundController {
    
	public void PlaySound (string sound, string type)
    {
        if (type == Constants.MUSIC && GameObject.FindGameObjectWithTag(Constants.MUTEMUSIC).GetComponent<Image>().sprite.name == "music - on")
        {
            AkSoundEngine.PostEvent(sound, GameObject.FindGameObjectWithTag(Constants.SOUND));
            return;
        }
        if (type == Constants.GAMESOUNDS && GameObject.FindGameObjectWithTag(Constants.MUTEGAMESOUND).GetComponent<Image>().sprite.name == "game sound - on")
        {
            AkSoundEngine.PostEvent(sound, GameObject.FindGameObjectWithTag(Constants.SOUND));
            return;
        }
    }
	
}
