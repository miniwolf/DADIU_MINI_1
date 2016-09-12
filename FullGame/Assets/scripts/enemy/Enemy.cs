﻿using UnityEngine;
using System.Collections;
/// <summary>
/// Set Nav Mesh Agent / stoppign distance to 2.4 (depending on a collider type the troll has)
/// In collider, set "Is Trigget to true"
/// </summary>
public class Enemy : MonoBehaviour {
    public float dist4SpeedIncrease;
    public float normalSpeed;
    public float accelerationFactor;

	// Use this for initialization
	public int stunTime = 2;
	[Tooltip("Name of the animation toggle to set to true")]
	public string moveAnimation;
	[Tooltip("Name of the animation toggle to set to true")]
	public string eatingAnimation;
	[Tooltip("Name of the animation toggle to set to true")]
	public string catchAnimation;

	private bool moving = true;

	// External components
	private ScoreInterface score;
	private GameObject following;

	// Personal components
	private NavMeshAgent navAgent;
	private Animator animator;

	void Start() {
		animator = GetComponentInChildren<Animator>();
		navAgent = GetComponent<NavMeshAgent>();

		score = GameObject.FindGameObjectWithTag(Constants.SCORE).GetComponent<ScoreInterface>();
		following = GameObject.FindGameObjectWithTag(Constants.PLAYER);
	}

	// Update is called once per frame
	void Update() {
		if ( following != null && moving) {
			navAgent.Resume(); // resume agent 
            UpdateSpeed();
            StartMoving();
			navAgent.destination = following.transform.position;
			gameObject.transform.LookAt(following.transform.position);
			// troll object model is 90 degrees off, which we fix by rotating it by 90 degree
			gameObject.transform.Rotate(new Vector3(0, 90, 0));
		} else {
			navAgent.Stop (); // stop agent from evaluating path
		}
	}

	void OnCollisionEnter(Collision collision) {
		if ( moving ) {
			if ( collision.gameObject.tag.Equals(Constants.CAKEICON) ) {
				StartCoroutine(StopMoving());
				score.AddTrollScore();
			}
		}

		if ( collision.gameObject.tag.Equals(Constants.CAKEICON) ) {
			StartCoroutine(StopMoving());
			score.AddTrollScore();
		} else if ( collision.gameObject.tag.Equals(Constants.PLAYER) ) {
			CatchGirl();
		}
	}

    private void UpdateSpeed() {
		if ( GetDistanceToGirl() > dist4SpeedIncrease ) {
			navAgent.speed = GetDistanceToGirl() / dist4SpeedIncrease * normalSpeed * (1 + accelerationFactor);
		} else {
			navAgent.speed = normalSpeed;
		}
    }

    public float GetDistanceToGirl() {
        return Vector3.Distance(following.transform.position, transform.position);
    }

	IEnumerator StopMoving() {
		moving = false;
		animator.SetBool(moveAnimation, false);
		StartEating();
		yield return new WaitForSeconds(stunTime);
		StopEating();
		StartMoving();
	}

	private void StartEating() {
		animator.SetBool(eatingAnimation, true);
	}

	private void StopEating() {
		animator.SetBool(eatingAnimation, false);
	}

	private void StartMoving() {
		moving = true;
		animator.SetBool(moveAnimation, true);
	}

	private void CatchGirl() {
		moving = false;
		following.GetComponent<Player>().GotCaught();
		animator.SetBool(moveAnimation, false);
		animator.SetTrigger(catchAnimation);
	}
}
