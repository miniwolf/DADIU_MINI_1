using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThrowingCake : MonoBehaviour, Cake {
	[Tooltip("Power of the ball")]
	[Range(10,100)]
	public int factor = 50;

	// TODO: Should be deleted
	//public Text debug;

	private Vector3 startPos;
	private Quaternion startRotation;

	private Transform player;
	private Rigidbody ballBody;
	private Camera playerCam;

	private bool isShooting = false;
	private bool mayThrow = false;

	private Animator animator;

	public void Start() {

		animator = GameObject.FindGameObjectWithTag(Constants.PLAYER).GetComponentInChildren<Animator>();

		ballBody = gameObject.GetComponent<Rigidbody>();
		startRotation = ballBody.rotation;

		playerCam = GameObject.FindGameObjectWithTag(Constants.PLAYERCAM).GetComponent<Camera>();
		player = GameObject.FindGameObjectWithTag(Constants.PLAYER).GetComponent<Transform>();
	}

	public void OnMouseDown() {
		down();
	}

	public void OnMouseUp() {
		up(Input.mousePosition);
		animator.SetTrigger ("Throw");
	}

	// Update is called once per frame
	public void Update() {
		
		RaycastHit hit;

		if ( Input.touchCount > 0 ) {
			Touch touch = Input.touches[0];
			if ( Physics.Raycast(playerCam.ScreenPointToRay(touch.position), out hit) 
				&& hit.transform.tag == Constants.CAKEICON ) {
				if ( touch.phase == TouchPhase.Began ) {
					down();
				}

			}
			if ( mayThrow && touch.phase == TouchPhase.Ended ) {
				up(touch.position);
			}
		}

		if ( !isShooting ) {
			transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);			
		}
	}

	private void up(Vector3 position) {
		if ( !isShooting ) {
			Vector3 endPos = playerCam.ScreenToWorldPoint(position);
			RaycastHit hit;
			if ( Physics.Raycast(playerCam.ScreenPointToRay(position), out hit) ) {
				endPos = hit.point;
			}
			endPos.y = 0;

			transform.parent = null;
			Vector3 force = endPos - transform.position;
			ballBody.constraints = RigidbodyConstraints.FreezePositionY;
			ballBody.AddForce(force * factor);
		}
		isShooting = true;
		mayThrow = false;
	}

	private void down() {
		mayThrow = true;
	}

	public void OnCollisionEnter() {
		Reset();
	}

	private void Reset() {
		gameObject.SetActive(PlayerPrefs.GetInt("numCakes") != 1);
		transform.rotation = startRotation;

		ballBody.constraints = RigidbodyConstraints.FreezeAll;
		ballBody.velocity = Vector3.zero;
		isShooting = false;
	}

	public bool MayThrow() {
		return mayThrow;
	}
}
