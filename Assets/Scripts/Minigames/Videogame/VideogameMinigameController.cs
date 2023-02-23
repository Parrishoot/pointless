using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideogameMinigameController : StateManager<VideogameMinigameController>, IMinigameController
{

    private VideogameSetupState setupState = new VideogameSetupState();

    public bool IsFinished()
    {
        return false;
    }

    public override void Start()
    {
        currentState = setupState;

        base.Start();
    }
}
