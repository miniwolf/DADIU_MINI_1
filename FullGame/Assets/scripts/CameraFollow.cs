using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject player;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x-15, player.transform.position.y + 15, player.transform.position.z-15);
	}
}
