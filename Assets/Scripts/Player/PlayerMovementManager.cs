using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : Singleton<PlayerMovementManager>
{

    public enum MOVEMENT_TYPE {
        BASE,
        LADDER
    }

    private PlayerBaseMovementController playerBaseMovementController;

    private PlayerLadderMovementController playerLadderMovementController;

    private PlayerMovementController currentMovementController;

    // Start is called before the first frame update
    void Start()
    {
        playerBaseMovementController = GetComponent<PlayerBaseMovementController>();
        playerLadderMovementController = GetComponent<PlayerLadderMovementController>();

        currentMovementController = playerBaseMovementController;
    }

    public void SwitchMovementController(MOVEMENT_TYPE movementType) {
        
        currentMovementController.Disable();
        
        switch(movementType) {

            case MOVEMENT_TYPE.BASE:
                currentMovementController = playerBaseMovementController;
                break;

            case MOVEMENT_TYPE.LADDER:
                currentMovementController = playerLadderMovementController;
                break;

        }

        currentMovementController.Enable();
    }

    public void DisableMovement() {
        currentMovementController.Disable();
    }

    public void EnableMovement() {
        currentMovementController.Enable();
    }

    public bool isGrounded() {
        return currentMovementController.isGrounded();
    }
}
