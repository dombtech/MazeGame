using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public static Animator animator;
    public static bool isRunning = false;
    private void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.DownArrow))
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");
            Vector3 moveVector = (Vector3.right * x + Vector3.forward * z);

            if (!isRunning)
            {
                isRunning = true;
                animator.SetBool("Run", true);
            }
            moveVector.Normalize();
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * 2 * Time.deltaTime, Space.World);
        }
        else
        {
            if (isRunning)
            {
                isRunning = false;
                animator.SetBool("Idle", true);
            }
        }
    }
}
