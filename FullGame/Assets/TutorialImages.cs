﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialImages : MonoBehaviour {

	public Sprite[] imgs = new Sprite[2];
	int iterator = 0;

	public void NextImage(){
		if (iterator < imgs.Length-1) {
			iterator++;
			GetComponent<Image> ().sprite = imgs [iterator];
		} else {
			iterator = 0;
			gameObject.GetComponent<Image> ().enabled = false;
		}
	}
}
