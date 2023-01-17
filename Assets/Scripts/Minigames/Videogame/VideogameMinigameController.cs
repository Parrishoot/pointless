using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideogameMinigameController : MinigameController
{

    private VideogameSetupState setupState = new VideogameSetupState();

    public void Start()
    {
        Debug.Log("Setting up setup state");
        currentState = setupState;

        base.Start();
    }
}
