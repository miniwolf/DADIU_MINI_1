using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	GameObject player;
	Quaternion camRotation;
	// Use this for initialization
	void Start () {
		player = GetComponentInParent<GameObject> ();
		camRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
		transform.rotation = camRotation;
	}
}
