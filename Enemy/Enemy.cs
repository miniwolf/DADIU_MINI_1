using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization

	private GameObject following;
	private bool moving;
	private NavMeshAgent navAgent;

	void Start () {
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (following != null && moving) {
			//transform.position = Vector3.MoveTowards(transform.position, following.transform.position, 0.3f);
			navAgent.destination = following.transform.position;
			transform.LookAt (navAgent.nextPosition);
		} else {
			navAgent.destination = gameObject.transform.position; // not move - set position to this object position
		}
	}

	public void StartFollowingGirl(GameObject target) {
		following = target;
		moving = true;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag.Equals(Constants.CAKE)) {
			Destroy (collision.gameObject);
			StartCoroutine (StopMoving());
		}
	}


	IEnumerator StopMoving() {
		moving = false;
		PlayEating ();
		yield return new WaitForSeconds (2.0f);

		StartMoving ();
	}

	private void PlayEating() {

	}

	private void StartMoving() {
		moving = true;
	}
}
