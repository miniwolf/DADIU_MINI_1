using UnityEngine;
using System.Collections;

public class Cake : MonoBehaviour {

	private Enemy enemy;

	// Use this for initialization
	void Start () {
		enemy = GameObject.FindObjectOfType<Enemy> ();
		if (enemy == null)
			Debug.Log ("Enemy not found for: " + this);
	}

	// Update is called once per frame
	void Update () {
		if(enemy != null)
			transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, (float) (Mathf.Abs(0.5f * Mathf.Sin(Time.timeSinceLevelLoad))));
	}
}
