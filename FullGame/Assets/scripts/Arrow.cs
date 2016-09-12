using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Arrow : MonoBehaviour, ArrowInterface {
	GameObject enemy,player;
	Camera cam;
	Vector3 dir;
	Vector3 screenDir;
	Vector3 pos;
	float angle;
	bool isEnemyVisible = false;

	// Use this for initialization
	void Start () {
		enemy = GameObject.FindGameObjectWithTag(Constants.ENEMY);
		player = GameObject.FindGameObjectWithTag(Constants.PLAYER);
		cam = GameObject.FindGameObjectWithTag(Constants.PLAYERCAM).GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update() {
		isEnemyVisible = GameObject.FindGameObjectWithTag("EnemyRenderer").GetComponent<Renderer>().isVisible;

		if ( !isEnemyVisible ) {
			DisplayArrow();
			GetDirAndPlaceArrow();
		} else {
			Debug.Log("Should remove");
			RemoveArrow();
		}
	}

	
	public void DisplayArrow(){
		gameObject.GetComponent<Image>().enabled = true;
	}
	
	void RemoveArrow(){
		gameObject.GetComponent<Image>().enabled = false;
	}

	void GetDirAndPlaceArrow(){
		pos = cam.WorldToViewportPoint(enemy.transform.position);
		pos.x = Mathf.Clamp (pos.x,0.1f,0.9f);
		pos.y = Mathf.Clamp (pos.y,0.1f,0.9f);
		pos.z = Mathf.Clamp (pos.z, 0.1f, 0.9f);
		transform.position = cam.ViewportToScreenPoint(pos);

		dir = enemy.transform.position - player.transform.position;
		angle = Mathf.Atan2 (dir.z,dir.x) * Mathf.Rad2Deg;
		gameObject.transform.rotation = Quaternion.AngleAxis (angle+90, Vector3.forward);
	}
}
