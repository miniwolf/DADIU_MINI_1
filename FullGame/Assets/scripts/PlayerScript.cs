using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

// TODO implements interface
public class PlayerScript : MonoBehaviour {
	public int speed = 6;
	public int angularSpeed = 120;
	public Score score;
	public CakesText cakeText;

	private NavMeshAgent agent;
	private Cake cakeIcon;
	private Camera playerCam;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;
		agent.angularSpeed = angularSpeed;

		cakeIcon = GameObject.FindGameObjectWithTag(Constants.CAKEICON).GetComponent<Cake>();
		playerCam = GameObject.FindGameObjectWithTag(Constants.PLAYERCAM).GetComponent<Camera>();
	}

	/// <summary>
	/// Moves the player agent to a selected position
	/// </summary>
	/// <param name="pos">Position selected in the scene</param>
	private void Move(Vector3 pos) {
		RaycastHit hit;
		if ( Physics.Raycast(playerCam.ScreenPointToRay(pos), out hit) ) {
			if ( hit.transform.tag != Constants.CAKEICON ) {
				agent.destination = hit.point;
			}
		}
	}

	// Update is called once per frame
	void Update() {
		if ( !cakeIcon.MayThrow() ) {	
			foreach ( Touch touch in Input.touches ) {
				Move(touch.position);
			}

			// TODO: currently only works on testing with right mouse button
			// These lines are only for testing.
			if ( Input.GetMouseButtonDown(1) ) {
				Move(Input.mousePosition);
			}
		}
	}

	/// <summary>
	/// Handles collisions with cake and laundry game objects
	/// Cake: Adds the cake on top of the player and updates the score
	/// Laundry: Removes the laundry and adds laundry score
	/// </summary>
	/// <param name="other">Other collider</param>
	void OnTriggerEnter(Collider other) {
		if ( other.gameObject.tag == Constants.CAKE ) {
			other.gameObject.SetActive(false); // TODO replace with Despawn method
			cakeText.AddCake();
		}

		if ( other.gameObject.tag == Constants.LAUNDRY ) {
			other.gameObject.SetActive(false); // TODO replace with Despawn method
			score.AddLaundryScore();
			// TODO speedup
		}
	}
}