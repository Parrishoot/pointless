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
        else if(PlayerMovementManager.GetInstance().isGrounded()) {
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
            if(PlayerMovementManager.GetInstance().isGrounded() && leftGround) {
                EndClimbing();
            }
            else if(!PlayerMovementManager.GetInstance().isGrounded()) {
                leftGround = true;
            }
        }
    }

    public void BeginClimbing() {

        // TODO: ADD SOME COOL ANIMATION HERE OR SOMETHING

        isClimbing = true;
        leftGround = false;
        floor.GetComponent<BoxCollider2D>().enabled = false;
        PlayerMovementManager.GetInstance().SwitchMovementController(PlayerMovementManager.MOVEMENT_TYPE.LADDER);
    }

    public void EndClimbing() {
        isClimbing = false;
        floor.GetComponent<BoxCollider2D>().enabled = true;
        PlayerMovementManager.GetInstance().SwitchMovementController(PlayerMovementManager.MOVEMENT_TYPE.BASE);        
    }
}
