using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseMovementController : PlayerMovementController
{

    public float movementSpeed;
    public float jumpForce;

    public Rigidbody2D playerRigidbody;

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.1)
        {
            playerRigidbody.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, playerRigidbody.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") < -0.1)
        {
            playerRigidbody.velocity = new Vector2(-movementSpeed * Time.fixedDeltaTime, playerRigidbody.velocity.y);  
        }
        else
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
        }

        if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.JUMP, true) && isGrounded())
        {
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }

    public override void Disable() {
        playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
        enabled = false;
    }

    public override void Enable() {
        playerRigidbody.velocity = new Vector2(0, 0);
        enabled = true;
    }
}
