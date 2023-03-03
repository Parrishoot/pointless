using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotateTest : MonoBehaviour
{
    public float rotateSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime);
    }
}
