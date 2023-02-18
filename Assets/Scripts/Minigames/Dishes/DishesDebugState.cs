using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesGameState : MinigameState<DishesMinigameController>
{

    public override void Setup(DishesMinigameController controller)
    {
        controller.gridManager.InitGrid();

        controller.SpawnDish(controller.gridManager.GetChunks()[0]);
        
        base.Setup(controller);
    }
}
