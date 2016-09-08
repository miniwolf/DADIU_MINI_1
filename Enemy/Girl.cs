using UnityEngine;
using System.Collections;

public class Girl : MonoBehaviour {

	private Enemy enemy;
	// Use this for initialization
	void Start () {
		enemy = GameObject.FindObjectOfType<Enemy> ();
		enemy.StartFollowingGirl (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (transform.position);
		Vector3 v = transform.position;
		transform.position = new Vector3((0.2f + Mathf.Sin(Time.realtimeSinceStartup)) + v.x, Mathf.PingPong(v.y, v.y + 100), v.z + (float)(Time.realtimeSinceStartup * 0.05));

		//transform.position = new Vector3(Mathf.PingPong((float)Time.time*3,9.9)-4.9, Mathf.PingPong((float)Time.time*3,7.1)-3.6,0);
	}
}
