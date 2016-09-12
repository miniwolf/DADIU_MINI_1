using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
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
		if ( following != null && moving ) {
			StartMoving();
			navAgent.destination = following.transform.position;
			navAgent.Resume();
			transform.LookAt(navAgent.nextPosition);
		} else {
			navAgent.destination = transform.position; // not move - set position to this object position
		}
	}

	void OnCollisionEnter(Collision collision) {
		if ( collision.gameObject.tag.Equals(Constants.CAKEICON) ) {
			StartCoroutine(StopMoving());
			score.AddTrollScore();
		}

		if ( collision.gameObject.tag.Equals(Constants.PLAYER) ) {
			CatchGirl();
		}
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
		animator.SetBool(moveAnimation, false);
		animator.SetTrigger(catchAnimation);
	}
}
