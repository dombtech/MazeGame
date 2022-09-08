using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCamera : MonoBehaviour
{

    public void Start(int dimension)
    {
        transform.localPosition = new Vector3((dimension / 2)-0.5F, dimension * 2.3F, (dimension/2)-0.5F);
    }
}
