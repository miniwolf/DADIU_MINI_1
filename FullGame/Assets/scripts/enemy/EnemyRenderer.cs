using UnityEngine;
using System.Collections;

public class EnemyRenderer : MonoBehaviour {
	GameObject arrow;

	void Start() {
		arrow = GameObject.FindGameObjectWithTag(Constants.ARROW);
	}

	void OnBecameInvisible() {
		if ( arrow != null ) {
			arrow.GetComponent<ArrowInterface>().DisplayArrow();
		}
	}
}
