using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CakeImpl : MonoBehaviour, Cake {
	private bool couldbeswipe;
	private Vector2 touchStart;
	private Vector3 worldAngle;
	private Vector2 touchEnd;
	private bool g; // gravity

	public int ballSpeed;
	public Camera cam;
	public GameObject ball;
	public Rigidbody ballBody;
	private int factor = 50;

	private Vector3 startPos;

	void Start() {
		GetComponent<Rigidbody>().detectCollisions = false;
	}

	public void OnMouseDown() {
		down(Input.mousePosition);
	}

	private void down(Vector3 position) {
		Debug.Log("Clicked down");
		startPos = ball.transform.position;
	}

	public void OnMouseUp() {
		up(Input.mousePosition);
	}

	private void up(Vector3 position) {
		Vector3 endPos = cam.ScreenToWorldPoint(position);
		RaycastHit hit;
		if ( Physics.Raycast(cam.ScreenPointToRay(position), out hit) ) {
			Debug.Log(hit.point);
			endPos = hit.point;
		}
		endPos.y = 0;

		Vector3 force = endPos - startPos;

		GetComponent<Rigidbody>().AddForce(force * factor);
	}
	
	// Update is called once per frame
	public void Update() {
		if ( Input.touchCount > 0 ) {
			Touch touch = Input.touches [0];
			switch (touch.phase) {
			case TouchPhase.Began:
				down(touch.position);
				break;
			case TouchPhase.Ended:
				up(touch.position);
				break;
			}
		}
	}

	public void ThrowCake(Vector3 touchEnd) {
		GetAngle();
		GetComponent<Rigidbody>().AddForce(new Vector3((worldAngle.x * ballSpeed), (worldAngle.y * ballSpeed), (worldAngle.z * ballSpeed)));
		/*
		float distance = Vector3.Distance(ball.transform.position, ballBody.transform.position); 
		GetComponent<Rigidbody>().AddForce(ball.transform.forward * 100); // shoot
*/
	}

	private void GetAngle() {
		worldAngle = cam.ScreenToWorldPoint(new Vector3(touchEnd.x, touchEnd.y + 800f, ((cam.nearClipPlane - 100f) * 1.8f)));
	}
}
