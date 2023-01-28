using UnityEngine;

public class LadderInteractable : Interactable
{
    public GameObject floor;

    private bool isClimbing = false;

    private bool leftGround = false;

    public override void Interact()
    {
        if(!isClimbing) {
            BeginClimbing();
        }
        else if(PlayerLadderMovementController.GetInstance().IsOnGround()) {
            EndClimbing();
        }
    }

    public override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);

        if (isClimbing && other.gameObject.tag == "Player")
        {
            EndClimbing();
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if(isClimbing) {
            if(PlayerLadderMovementController.GetInstance().IsOnGround() && leftGround) {
                EndClimbing();
            }
            else if(!PlayerLadderMovementController.GetInstance().IsOnGround()) {
                leftGround = true;
            }
        }
    }

    public void BeginClimbing() {

        // TODO: ADD SOME COOL ANIMATION HERE OR SOMETHING

        isClimbing = true;
        leftGround = false;
        floor.GetComponent<BoxCollider2D>().enabled = false;
        PlayerMovementController.GetInstance().Disable();
        PlayerLadderMovementController.GetInstance().Enable();
    }

    public void EndClimbing() {
        isClimbing = false;
        floor.GetComponent<BoxCollider2D>().enabled = true;
        PlayerLadderMovementController.GetInstance().Disable(); 
        PlayerMovementController.GetInstance().Enable();         
    }
}
