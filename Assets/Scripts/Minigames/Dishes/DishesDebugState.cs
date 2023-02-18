using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesGameState : MinigameState<DishesMinigameController>
{

    public override void Setup(DishesMinigameController controller)
    {
        controller.gridManager.InitGrid();

        List<ChunkMeta> dishesToSpawn = controller.gridManager.GetChunksOfType(ChunkMeta.ChunkType.DISH);
        List<GameObject> dishControllerObjects = controller.dishSpawner.SpawnWithinBounds(controller.gameObject, dishesToSpawn.Count);

        for(int i = 0; i < dishesToSpawn.Count; i++) {
            DishController newDishController = dishControllerObjects[i].GetComponent<DishController>();
            newDishController.Init(dishesToSpawn[i], controller.gridManager);
        }

        controller.gridManager.FilterGrid();
        controller.gridManager.DisplayGrid();
        
        base.Setup(controller);
    }
}
