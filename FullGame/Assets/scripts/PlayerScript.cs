using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class PlayerScript : MonoBehaviour, Player {
	public int speed = 6;
	public int speedup;
	public int slowdown;
	public int angularSpeed = 120;
	public int speedupTime = 1;
	public int slowdownTime = 1;

	// External components
	private Camera cam;
	private Score score;
	private CakesText cakeText;
	private Cake cakeThrowing;

	// Internal components
	private NavMeshAgent agent;
	private Stopwatch timer;
	private bool hasBeenCaught;
    private float now;

	Animator animator;
	public float animatorSpeedUp;
	// Use this for initialization
	void Start() {
		animator = gameObject.GetComponentInChildren<Animator> ();
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;
		agent.angularSpeed = angularSpeed;

		cakeText = GameObject.FindGameObjectWithTag(Constants.CAKETEXT).GetComponent<CakesText>();
		score = GameObject.FindGameObjectWithTag(Constants.SCORE).GetComponent<Score>();
		cakeThrowing = GameObject.FindGameObjectWithTag(Constants.CAKEICON).GetComponent<ThrowingCake>();
		cam = GameObject.FindGameObjectWithTag(Constants.PLAYERCAM).GetComponent<Camera>();
		AkSoundEngine.PostEvent("auntieScream", GameObject.FindGameObjectWithTag(Constants.SOUND));
        now = Time.time;
	}

	/// <summary>
	/// Moves the player agent to a selected position
	/// </summary>
	/// <param name="pos">Position selected in the scene</param>
	private void Move(Vector3 pos) {
		RaycastHit hit;
        if ( Physics.Raycast(cam.ScreenPointToRay(pos), out hit) ) {
			if ( hit.transform.tag != Constants.CAKEICON ) {
				agent.destination = hit.point;
			}
		}
	}

	// Update is called once per frame
	void Update() {
		if ( hasBeenCaught ) {
			return;
		}

		if (agent.remainingDistance > 0.1) {
			gameObject.transform.GetComponentInChildren<Animator> ().SetBool ("isMoving", true);
            if (Time.time - now > 0.25) {
                AkSoundEngine.PostEvent("auntieFootstep", GameObject.FindGameObjectWithTag(Constants.SOUND));
                now = Time.time;
            }
        } else {
			gameObject.transform.GetComponentInChildren<Animator> ().SetBool ("isMoving", false);
		}

		ToggleMoving(agent.remainingDistance > 0.1);

		foreach ( Touch touch in Input.touches ) {
			Move(touch.position);
        }

		// TODO: currently only works on testing with right mouse button
		// These lines are only for testing.
		if ( Input.GetMouseButtonDown(1) ) {
            AkSoundEngine.PostEvent("auntieMoveClick", GameObject.FindGameObjectWithTag(Constants.SOUND));
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

            AkSoundEngine.PostEvent("auntiePickUpCake", GameObject.FindGameObjectWithTag(Constants.SOUND));
			cakeText.AddCake();
			cakeThrowing.SetActive();
		}

		if ( other.gameObject.tag == Constants.LAUNDRY ) {
			other.gameObject.SetActive(false); // TODO replace with Despawn method
			score.AddLaundryScore();
			AkSoundEngine.PostEvent("auntiePickUpClothes", GameObject.FindGameObjectWithTag(Constants.SOUND));
			// speedup
			StartCoroutine(SpeedUp());
		}

		if( other.gameObject.tag == Constants.MUSHROOM) {
			other.gameObject.SetActive(false); // TODO replace with Despawn method
			StartCoroutine(SlowDown()); // Slowdown
		}
	}

	IEnumerator SpeedUp() {
		agent.speed += speedup;
		animator.speed += animatorSpeedUp;
		yield return new WaitForSeconds(speedupTime);

		agent.speed -= speedup;
		animator.speed -= animatorSpeedUp;
	}

	IEnumerator SlowDown() {
		agent.speed -= slowdown;
		yield return new WaitForSeconds(slowdownTime);

		agent.speed += slowdown;
	}

	private void ToggleMoving(bool isMoving) {
		transform.GetComponentInChildren<Animator>().SetBool("isMoving", isMoving);
	}

	public void GotCaught() {
		hasBeenCaught = true;
		ToggleMoving(false);
	}
}
