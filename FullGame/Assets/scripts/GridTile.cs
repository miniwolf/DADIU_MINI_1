using UnityEngine;
using System.Collections;

public class GridTile {
	public Vector2 pos;
	public bool isObstructed;
	public Rect rect;

	public GridTile() {
		pos = Vector2.zero;
		isObstructed = false;
		rect = new Rect ();
	}

	public bool CheckIfObstructed() {
		RaycastHit[] hit;
		hit = Physics.BoxCastAll(new Vector3(rect.center.x, 40, rect.center.y), 
								 new Vector3(rect.size.x * .5f, 1, rect.size.y * .5f), 
								 -Vector3.up, Quaternion.identity, 45f);

		for ( int i = 0; i < hit.Length; i++ ) {
			if ( hit[i].transform.tag == Constants.OBSTACLE
				|| hit[i].transform.tag == Constants.PLAYER
				|| hit[i].transform.tag == Constants.ENEMY) {
				isObstructed = true;
			}
		}
		return isObstructed;
	}
}
