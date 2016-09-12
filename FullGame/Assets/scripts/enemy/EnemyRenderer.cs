using UnityEngine;
using System.Collections;

public class EnemyRenderer : MonoBehaviour {
	void OnBecameInvisible() {
		GameObject arrowObject = GameObject.FindGameObjectWithTag(Constants.ARROW);
		ArrowInterface aInt = arrowObject.GetComponent<ArrowInterface>();
		aInt.DisplayArrow();
	}
}

