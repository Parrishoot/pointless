using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    public float turnSpeed = 4.0f;

    private float rotX;

    private Vector3 startingEulerAngles;

    public void Start() {
        // startingEulerAngles = transform.eulerAngles;
    }

    private void Update() {

        
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
    
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, -90f, 90f);
    
        
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }
}
