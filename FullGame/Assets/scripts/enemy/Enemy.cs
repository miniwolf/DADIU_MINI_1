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

	// we are using this to avoid jittering of troll when he is close to girl or obstacle
	public bool collidedWithGirl; 

	void Start() {
		animator = GetComponentInChildren<Animator>();
		navAgent = GetComponent<NavMeshAgent>();

		score = GameObject.FindGameObjectWithTag(Constants.SCORE).GetComponent<ScoreInterface>();
		following = GameObject.FindGameObjectWithTag(Constants.PLAYER);
	}

	// Update is called once per frame
	void Update() {
		if ( following != null && moving && !collidedWithGirl) {
			navAgent.Resume(); // resume agent 
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
		following.GetComponent<Player>().GotCaught();
		animator.SetBool(moveAnimation, false);
		animator.SetTrigger(catchAnimation);
	}
}
