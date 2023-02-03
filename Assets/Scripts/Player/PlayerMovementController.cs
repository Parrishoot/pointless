using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementController : MonoBehaviour
{

    public GroundChecker groundChecker;

    public bool isGrounded() {
        return groundChecker.IsGrounded();
    }

    public abstract void Enable();

    public abstract void Disable();

}
