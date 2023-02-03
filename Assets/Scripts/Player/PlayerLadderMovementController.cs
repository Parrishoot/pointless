using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderMovementController : PlayerMovementController
{
    public Rigidbody2D playerRigidbody;

    private float gravityScale;

    public long ladderMovementSpeed;

    public void FixedUpdate() {
        if (Input.GetAxisRaw("Vertical") > 0.1) {
            playerRigidbody.velocity = new Vector2(0, ladderMovementSpeed * Time.fixedDeltaTime);
        }
        else if (Input.GetAxisRaw("Vertical") < -0.1) {
            playerRigidbody.velocity = new Vector2(0, -ladderMovementSpeed * Time.fixedDeltaTime);
        }
        else {
            playerRigidbody.velocity = new Vector2(0, 0);
        }
    }

    public override void Disable() {
        playerRigidbody.velocity = new Vector2(0, 0);
        playerRigidbody.gravityScale = gravityScale;
        enabled = false;
    }

    public override void Enable() {
        playerRigidbody.velocity = new Vector2(0, 0);
        gravityScale = playerRigidbody.gravityScale;
        playerRigidbody.gravityScale = 0f;
        enabled = true;
    }
}
