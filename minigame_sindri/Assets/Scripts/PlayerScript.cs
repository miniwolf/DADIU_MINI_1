﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;

// TODO implements interface
public class PlayerScript : MonoBehaviour {
	public int speed = 3;
	public int angularSpeed = 120;
	public Camera cam;

	private NavMeshAgent agent;
	private string cakeTag = "Cake";
	private string laundryTag = "Laundry";
	private Stopwatch timer;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;
		agent.angularSpeed = angularSpeed;

		PlayerPrefs.SetInt("highscore", 0);
	}

	/// <summary>
	/// Moves the player agent to a selected position
	/// </summary>
	/// <param name="pos">Position selected in the scene</param>
	void Move(Vector3 pos) {
		RaycastHit hit;
		if ( Physics.Raycast(cam.ScreenPointToRay(pos), out hit) ) {
			agent.destination = hit.point;
		}
	}

	public void OnMouseDown() {
		Move(Input.mousePosition);
	}

	// Update is called once per frame
	void Update() {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			foreach(Touch touch in Input.touches) {
				Move(touch.position);
			}
		}

		if (Input.GetMouseButtonDown(1)) {
			Move(Input.mousePosition);
		}
	}

	/// <summary>
	/// Handles collisions with cake and laundry game objects
	/// Cake: Adds the cake on top of the player and updates the score
	/// Laundry: Removes the laundry and adds laundry score
	/// </summary>
	/// <param name="other">Other collider</param>
	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == cakeTag) {
			other.gameObject.SetActive(false); // TODO replace with Despawn method
			//AddCake();
			//AddCakeScore();
		}

		if (other.gameObject.tag == laundryTag) {
			other.gameObject.SetActive(false); // TODO replace with Despawn method
			//AddLaundryScore();
			// TODO speedup
		}
	}
}