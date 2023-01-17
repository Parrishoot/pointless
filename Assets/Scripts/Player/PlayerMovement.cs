using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed;
    public float jumpForce;

    private Rigidbody2D rigidbody;
    public GroundChecker groundChecker;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // TODO: (Maybe) move this to InputManager instead
        if(!MinigameManager.GetInstance().InProgress())
        {
            if (Input.GetAxisRaw("Horizontal") > 0.1)
            {
                rigidbody.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, rigidbody.velocity.y);
            }
            else if (Input.GetAxisRaw("Horizontal") < -0.1)
            {
                rigidbody.velocity = new Vector2(-movementSpeed * Time.fixedDeltaTime, rigidbody.velocity.y);
            }
            else
            {
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }

            if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.JUMP, true) && groundChecker.IsGrounded())
            {
                rigidbody.AddForce(new Vector2(0, jumpForce));
            }
        }
    }
}
