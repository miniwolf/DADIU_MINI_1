using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Arrow : MonoBehaviour, ArrowInterface {
	private GameObject enemy, player;
	private Camera cam;

	// Use this for initialization
	void Start() {
		enemy = GameObject.FindGameObjectWithTag(Constants.ENEMY);
		player = GameObject.FindGameObjectWithTag(Constants.PLAYER);
		cam = GameObject.FindGameObjectWithTag(Constants.PLAYERCAM).GetComponent<Camera>();
		gameObject.GetComponent<Image>().enabled = true;
	}

	// Update is called once per frame
	void Update() {
		bool isEnemyVisible = GameObject.FindGameObjectWithTag("EnemyRenderer").GetComponent<Renderer>().isVisible;
		if ( isEnemyVisible ) {
			ToggleArrow(false);
		}

		GetDirAndPlaceArrow();
	}

	private void ToggleArrow(bool arrowShown) {
		gameObject.GetComponent<Image>().enabled = arrowShown;
	}
	
	public void DisplayArrow() {
		ToggleArrow(true);
	}

	void GetDirAndPlaceArrow(){
		Vector3 pos = cam.WorldToViewportPoint(enemy.transform.position);
		pos.x = Mathf.Clamp(pos.x, .1f, .9f);
		pos.y = Mathf.Clamp(pos.y, .1f, .8f);
		pos.z = Mathf.Clamp(pos.z, .1f, .9f);
		transform.position = cam.ViewportToScreenPoint(pos);

		Vector3 dir = enemy.transform.position - player.transform.position;
		float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
		gameObject.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
	}
}
