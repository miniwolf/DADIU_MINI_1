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
	public GameObject parent;
	public Text debug;

	private Vector3 startPos;
	private Rigidbody rigid;

	void Start() {
		ballBody.freezeRotation = true;
	}

	public void OnMouseDown() {
		Debug.Log("Here");
		down(Input.mousePosition);
	}

	private void down(Vector3 position) {
		startPos = ball.transform.position;
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
		Vector3 force = endPos - startPos;
		ballBody.freezeRotation = false;
		ballBody.constraints = RigidbodyConstraints.FreezePositionY;
		ballBody.AddForce(force * factor);
	}
	
	// Update is called once per frame
	public void Update() {
		if ( Input.touchCount > 0 ) {
			Touch touch = Input.touches[0];
			if ( touch.phase == TouchPhase.Began ) {
				down(touch.position);
			} else if ( touch.phase == TouchPhase.Ended ) {
				up(touch.position);
			}
		}
	}

	void OnCollisionEnter() {
		Reset();
	}

	private void Reset() {
		transform.parent = parent.transform;
		gameObject.SetActive(PlayerPrefs.GetInt("numCakes") != 1);
		transform.position = new Vector3(parent.transform.position.x, transform.position.y, parent.transform.position.z);
		transform.rotation = new Quaternion();

		ballBody.constraints = RigidbodyConstraints.FreezePosition;
		ballBody.freezeRotation = true;
		ballBody.velocity = new Vector3();
	}
}
