using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThrowingCake : MonoBehaviour, Cake {
	[Tooltip("Power of the ball")]
	[Range(1000, 10000)]
	public int speed = 150;

	private LayerMask layerMask;

	private Vector3 startPos;
	private Quaternion startRotation;

	private Transform player;
	private Rigidbody ballBody;
	private Camera playerCam;

	private bool isShooting = false;
	private bool mayThrow = false;

	private Animator animator;
	private CakesTextInterface cakeText;

	public void Start() {
		layerMask = 1 << LayerMask.NameToLayer(Constants.GROUNDLAYER);

		animator = GameObject.FindGameObjectWithTag(Constants.PLAYER).GetComponentInChildren<Animator>();

		ballBody = gameObject.GetComponent<Rigidbody>();
		startRotation = ballBody.rotation;

		playerCam = GameObject.FindGameObjectWithTag(Constants.PLAYERCAM).GetComponent<Camera>();
		player = GameObject.FindGameObjectWithTag(Constants.PLAYER).GetComponent<Transform>();
		cakeText = GameObject.FindGameObjectWithTag(Constants.CAKETEXT).GetComponent<CakesTextInterface>();
		
		gameObject.SetActive(false);
	}

	public void OnMouseDown() {
		down(Input.mousePosition);
	}

	public void OnMouseUp() {
		up(Input.mousePosition);
		animator.SetTrigger("Throw");
        AkSoundEngine.PostEvent("auntieThrow", GameObject.FindGameObjectWithTag(Constants.SOUND));
    }
	
	public void Update() {
		RaycastHit hit;

		if ( Input.touchCount > 0 ) {
			Touch touch = Input.touches[0];
			if ( Physics.Raycast(playerCam.ScreenPointToRay(touch.position), out hit)
				&& hit.transform.tag == Constants.CAKEICON ) {
				if ( touch.phase == TouchPhase.Began ) {
					down(touch.position);
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
			if ( Physics.Raycast(playerCam.ScreenPointToRay(position), out hit, 10000f, layerMask) ) {
				endPos = hit.point;
			}
			endPos.y = 0;

			transform.parent = null;
			Vector3 force = endPos - startPos;
			ballBody.constraints = RigidbodyConstraints.FreezePositionY;
			ballBody.AddForce(force.normalized * speed);
		}

		cakeText.RemoveCake();
		isShooting = true;
		mayThrow = false;
	}

	private void down(Vector3 position) {
		RaycastHit hit;
		if ( Physics.Raycast(playerCam.ScreenPointToRay(position), out hit, 10000f, layerMask) ) {
			startPos = hit.point;
		}
		startPos.y = 0;
		mayThrow = true;
	}

	public void OnCollisionStay() {
		if ( isShooting ) {
			Reset();
		}
	}

	private void Reset() {
		if ( cakeText.GetNumCakes() == 0 ) {
			gameObject.SetActive(false);
		}
		transform.rotation = startRotation;
		ballBody.constraints = RigidbodyConstraints.FreezeAll;
		isShooting = false;
		mayThrow = false;
	}

	public void SetActive() {
		gameObject.SetActive(true);
	}

	public bool MayThrow() {
		return mayThrow;
	}
}
