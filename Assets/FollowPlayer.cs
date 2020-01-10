using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.localPosition = player.transform.localPosition - offset;
        transform.LookAt(player.transform);
	}
}
