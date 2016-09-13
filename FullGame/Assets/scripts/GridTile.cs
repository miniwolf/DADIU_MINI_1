using UnityEngine;
using System.Collections;


public class GridTile {
	public Vector2 pos;
	public bool isObstructed;
	public Rect rect;

	public GridTile(){
		pos = Vector2.zero;
		isObstructed = false;
		rect = new Rect ();

	}

	public bool CheckIfObstructed(){
		RaycastHit[] hit;
		hit = Physics.BoxCastAll (new Vector3 (rect.center.x, 40, rect.center.y), new Vector3(rect.size.x / 2,1,rect.size.y/2), -Vector3.up, Quaternion.identity, 45f);


		for (int i = 0; i < hit.Length; i++) {
			//Debug.Log (hit [i].transform.name);
			if (hit[i].transform.tag == "Obstacle"||hit[i].transform.tag == "Player"||hit[i].transform.tag=="Enemy") {
				//Debug.Log (hit[i].transform.name);
				isObstructed = true;
				//Debug.Log (rect.center);
				Debug.Log (isObstructed);
			}
		}
		return isObstructed;
		
	}
}
			
