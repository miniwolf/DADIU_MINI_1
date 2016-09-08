using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject player;
	private Quaternion camRotation;
	private float cameraZ = -13.9f;

	// Use this for initialization
	void Start () {
		//camRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x-15, player.transform.position.y + 15, player.transform.position.z-15);
		//transform.rotation = camRotation;
	}
}
