using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {


	// Use this for initialization
	void Start () {
        GameManager.Instance.RegisterGem();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider obj)
    {
	    if (!obj.gameObject.CompareTag("Player")) return;
	    GameManager.Instance.CollectGem();
	    Destroy(gameObject);
    }
}
