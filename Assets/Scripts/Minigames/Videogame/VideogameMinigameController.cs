using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideogameMinigameController : MinigameController<VideogameMinigameController>
{

    private VideogameSetupState setupState = new VideogameSetupState();

    public override void Start()
    {
        currentState = setupState;

        base.Start();
    }
}
