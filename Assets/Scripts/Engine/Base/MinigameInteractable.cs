using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInteractable : Interactable
{
    public MinigameManager.MINIGAME minigameType;
    public CameraController.POSITION cameraPosition;

    public override void Interact()
    {
        MinigameManager.GetInstance().SwapMinigameState(minigameType, cameraPosition);
    }

    
}
