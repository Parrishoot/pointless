using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesMinigameController : MinigameController
{

    public int gridSize;
    
    public GridManager gridManager;

    public DishesDebugState debugState;

    public override void Start()
    {
        debugState = new DishesDebugState(gridManager, gridSize);

        currentState = debugState;

        base.Start();
    }

}
