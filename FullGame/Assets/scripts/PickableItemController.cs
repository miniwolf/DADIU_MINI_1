using UnityEngine;
using System.Collections;

public class PickableItemController : MonoBehaviour {
	GridInterface grid;

	void Start() {
		grid = GameObject.FindGameObjectWithTag("PlaneGround").GetComponent<GridInterface> ();
	}

	void Update () {
        foreach ( Transform item in GetComponentsInChildren<Transform>(true) ) { 
            if ( !item.gameObject.activeSelf ) {
                item.gameObject.SetActive(true);
                item.gameObject.transform.position = GetGoodPosition();
            }
        }
    }

    Vector3 GetGoodPosition() {
		return grid.getSpawnPoint();
    }
}
