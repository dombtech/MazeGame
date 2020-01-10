using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            GameManager.Instance.LevelComplete();
        }
    }
}
