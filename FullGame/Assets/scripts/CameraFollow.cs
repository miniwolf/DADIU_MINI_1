using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	private GameObject player;

	void Start() {
		player = GameObject.FindGameObjectWithTag(Constants.PLAYER);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = player.transform.position;
		transform.position = new Vector3(playerPos.x - 15, playerPos.y + 12, playerPos.z);
	}
}
