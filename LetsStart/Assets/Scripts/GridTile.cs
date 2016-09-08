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
		if(Physics.Raycast(new Vector3(rect.center.x,20,rect.center.y),-Vector3.up,out hit,25)){
			if (hit.transform.tag != "Obstacle") {
				isObstructed = false;
			} else {
				isObstructed = true;
			}
		}
	}

	public void ObstructionPlaced(int x, int y){
		
	}



}
