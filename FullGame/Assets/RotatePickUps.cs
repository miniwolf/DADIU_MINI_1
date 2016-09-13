using UnityEngine;
using System.Collections;

public class RotatePickUps : MonoBehaviour {

	public float rotateSpeed = 20;

	void Start () {
	
	}

	void Update () {
		transform.Rotate ( Vector3.up * rotateSpeed * Time.deltaTime );
		
	}
}
