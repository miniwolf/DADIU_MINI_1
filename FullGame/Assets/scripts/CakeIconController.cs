using UnityEngine;
using System.Collections;

public class CakeIconController : MonoBehaviour {
    
    void Update()
    {
        Vector3 pos = GameObject.Find("Player").GetComponent<Rigidbody>().transform.position;
        gameObject.transform.position = new Vector3(pos.x, pos.y + 1, pos.z);
        if (PlayerPrefs.GetInt("numCakes") == 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
