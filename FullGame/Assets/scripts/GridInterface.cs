using UnityEngine;
using System.Collections;

public interface GridInterface {
	Vector2 getGridPos();
	Vector3 getWorldPos(int x, int y);
	Vector3 getSpawnPoint();
	void ObstructionPlaced(int x, int y);
	void NoLongerObstructed(int x, int y);
	Vector2 WorldtoGridPos(Vector3 worldPos);
	void OnDrawGizmos();
}
