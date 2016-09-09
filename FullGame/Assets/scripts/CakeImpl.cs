using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CakeImpl : MonoBehaviour {
	private bool couldbeswipe;
	private Vector2 touchStart;
	private Vector3 worldAngle;
	private Vector2 touchEnd;
	private bool g; // gravity
	private int factor = 50;

	public int ballSpeed;
	public Camera cam;
	public GameObject ball;
	public Rigidbody ballBody;
	public Text debug;
	public Transform player;

	private Vector3 startPos;
	private Rigidbody rigid;
	private Quaternion startRotation;

	private bool isShooting = false;
	private bool mayThrow = false;

	void Start() {
		startRotation = ballBody.rotation;
		ballBody.freezeRotation = true;
	}

	public void OnMouseDown() {
		down();
	}

	public void OnMouseUp() {
		up(Input.mousePosition);
	}

	private void up(Vector3 position) {
		Vector3 endPos = cam.ScreenToWorldPoint(position);
		RaycastHit hit;
		if ( Physics.Raycast(cam.ScreenPointToRay(position), out hit) ) {
			endPos = hit.point;
		}
		endPos.y = 0;

		transform.parent = null;
		Vector3 force = endPos - ball.transform.position;
		ballBody.freezeRotation = false;
		ballBody.constraints = RigidbodyConstraints.FreezePositionY;
		ballBody.AddForce(force * factor);
		isShooting = true;
		mayThrow = false;
	}

	void down() {
		mayThrow = true;
	}
	
	// Update is called once per frame
	public void Update() {
		RaycastHit hit;

		if ( Input.touchCount > 0 ) {
			Touch touch = Input.touches[0];
			if ( Physics.Raycast(cam.ScreenPointToRay(touch.position), out hit) 
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

	void OnCollisionEnter() {
		Reset();
	}

	private void Reset() {
		gameObject.SetActive(PlayerPrefs.GetInt("numCakes") != 1);
		transform.rotation = startRotation;

		ballBody.constraints = RigidbodyConstraints.FreezePosition;
		ballBody.freezeRotation = true;
		ballBody.velocity = Vector3.zero;
		ballBody.rotation = startRotation;
		isShooting = false;
	}
}
