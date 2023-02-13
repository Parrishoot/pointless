using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesMinigameController : MinigameController
{
    
    public DishesDebugState debugState = new DishesDebugState();

    public override void Start()
    {
        currentState = debugState;

        base.Start();
    }

}
