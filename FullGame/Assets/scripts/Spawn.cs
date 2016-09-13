using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	GridInterface gridInterface;

	void Start() {
		gridInterface = GameObject.FindGameObjectWithTag ("PlaneGround").GetComponent<GridInterface>();
	}

	///<summary>
	/// Disable cake icon from the girl's head (no more cakes in the inventory).
	///</summary>
    public void Despawn() {
		int gridX, gridY;
		Vector2 gridPos = gridInterface.WorldtoGridPos(gameObject.transform.position);
		gridX = Mathf.RoundToInt(gridPos.x);
		gridY = Mathf.RoundToInt(gridPos.y);
		gridInterface.NoLongerObstructed(gridX, gridY);
        gameObject.SetActive(false);
    }
}
