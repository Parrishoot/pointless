using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{

    private bool onGround = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall_B")
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall_B")
        {
            onGround = false;
        }
    }

    public bool IsGrounded()
    {
        return onGround;
    }
}
