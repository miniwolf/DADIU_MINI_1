﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public GameObject following;
	public Cake cake;

	// Use this for initialization
	private const int ENEMY_CAKE_HIT_DELAY_SECONDS = 2;
	private bool moving = true;
	private NavMeshAgent navAgent;

	void Start() {
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update() {
		if ( following != null && moving ) {
			//transform.position = Vector3.MoveTowards(transform.position, following.transform.position, 0.3f);
			navAgent.destination = following.transform.position;
			navAgent.Resume();
			transform.LookAt(navAgent.nextPosition);
		} else {
			navAgent.destination = transform.position; // not move - set position to this object position
		}
	}

	void OnCollisionEnter(Collision collision) {
		if ( collision.gameObject.tag.Equals(Constants.CAKE) ) {
			StartCoroutine(StopMoving());
		}
	}


	IEnumerator StopMoving() {
		moving = false;
		PlayEating();
		yield return new WaitForSeconds(ENEMY_CAKE_HIT_DELAY_SECONDS);

		StartMoving();
	}

	private void PlayEating() {

	}

	private void StartMoving() {
		moving = true;
	}
}
