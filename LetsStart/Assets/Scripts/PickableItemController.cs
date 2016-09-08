using UnityEngine;
using System.Collections;

public class PickableItemController : MonoBehaviour {

	GridInterface gridInterface;


	void Start(){
		gridInterface = GameObject.FindGameObjectWithTag ("PlaneGround").GetComponent<GridInterface> ();
	}


	void Update () {
        foreach (Transform item in gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (!item.gameObject.activeSelf)
            {
                item.gameObject.SetActive(true);
                item.gameObject.transform.position = GetGoodPosition();
            }
        }
    }

    Vector3 GetGoodPosition()
    {
		return gridInterface.getSpawnPoint ();
    }



}
