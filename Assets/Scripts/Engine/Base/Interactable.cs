using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{

    public abstract void Interact();

    private DayManager dayManager;

    private bool canInteract = false;

    void Start() {
        dayManager = DayManager.GetInstance();
    }

    public virtual void Update()
    {
        if(InputManager.GetInstance().GetKeyDown(InputManager.ACTION.INTERACT) && canInteract && !dayManager.IsWakingUp())
        {
            Interact();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canInteract = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInteract = false;
        }
    }
}
