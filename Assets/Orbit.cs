using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public GameObject ball;
    Vector3 moveVector;
    Vector3 localR;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.localPosition = ball.transform.localPosition;
        //if (Input.GetKey(KeyCode.D))
        //    localR.x -= 4;
        //if (Input.GetKey(KeyCode.A))
        //    localR.x += 4;
        //localR.y = Mathf.Clamp(localR.y, 2f, 90f);
        //Quaternion q = Quaternion.Euler(localR.y, localR.x, 0);
        //transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, q, Time.deltaTime * 4);
    }
}
