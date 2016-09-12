using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

// TODO implements interface
public class PlayerScript : MonoBehaviour, Player {
	public int speed = 6;
	public int angularSpeed = 120;
	public int speedupTime = 1;

	// External components
	private Camera cam;
	private Score score;
	private CakesText cakeText;
	private Cake cakeThrowing;

	// Internal components
	private NavMeshAgent agent;
	private Stopwatch timer;

	private int speedup;
	private bool hasBeenCaught;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;
		agent.angularSpeed = angularSpeed;
		speedup = 2 * speed;

		cakeText = GameObject.FindGameObjectWithTag(Constants.CAKETEXT).GetComponent<CakesText>();
		score = GameObject.FindGameObjectWithTag(Constants.SCORE).GetComponent<Score>();
		cakeThrowing = GameObject.FindGameObjectWithTag(Constants.CAKEICON).GetComponent<ThrowingCake>();
		cam = GameObject.FindGameObjectWithTag(Constants.PLAYERCAM).GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update() {
		if ( hasBeenCaught ) {
			return;
		}

		ToggleMoving(agent.remainingDistance > 0.1);

		foreach ( Touch touch in Input.touches ) {
			Move(touch.position);
		}

		// TODO: currently only works on testing with right mouse button
		// These lines are only for testing.
		if ( Input.GetMouseButtonDown(1) ) {
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
		if ( other.gameObject.tag == Constants.CAKE ) {
			other.gameObject.SetActive(false); // TODO replace with Despawn method
			cakeText.AddCake();
			cakeThrowing.SetActive();
		}

		if ( other.gameObject.tag == Constants.LAUNDRY ) {
			other.gameObject.SetActive(false); // TODO replace with Despawn method
			score.AddLaundryScore();
			StartCoroutine(SpeedUp()); // Speedup
		}
	}

	IEnumerator SpeedUp() {
		agent.speed += speedup;
		yield return new WaitForSeconds(speedupTime);

		agent.speed -= speedup;
	}

	/// <summary>
	/// Moves the player agent to a selected position
	/// </summary>
	/// <param name="pos">Position selected in the scene</param>
	private void Move(Vector3 pos) {
		RaycastHit hit;
		if ( Physics.Raycast(cam.ScreenPointToRay(pos), out hit) ) {
			if(hit.transform.tag!=Constants.CAKE)
				agent.destination = hit.point;
		}
	}

	private void ToggleMoving(bool isMoving) {
		transform.GetComponentInChildren<Animator>().SetBool("isMoving", isMoving);
	}

	public void GotCaught() {
		hasBeenCaught = true;
		ToggleMoving(false);
	}
}
