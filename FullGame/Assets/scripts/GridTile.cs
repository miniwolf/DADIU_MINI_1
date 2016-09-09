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

	public void CheckIfObstructed(){
		RaycastHit hit;
		if(Physics.Raycast(new Vector3(rect.center.x,40,rect.center.y),-Vector3.up,out hit,45)){
			if (hit.transform.tag != "Obstacle"&&hit.transform.tag != "Player"&&hit.transform.tag!="Enemy") {
				isObstructed = false;
			} else {
				isObstructed = true;
			}
		}
	}





}
