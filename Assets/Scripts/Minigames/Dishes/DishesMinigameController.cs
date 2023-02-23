using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesMinigameController : MonoBehaviour, IMinigameController
{
    
    public GridManager gridManager;

    public GameObject dishPreb;

    public List<DishController> dishControllers;

    public Spawner dishSpawner;

    public bool IsFinished()
    {
        return gridManager.Solved();
    }

    public void Start()
    {
        gridManager.InitGrid();

        List<ChunkMeta> dishesToSpawn = gridManager.GetChunksOfType(ChunkMeta.ChunkType.DISH);
        List<GameObject> dishControllerObjects = dishSpawner.SpawnWithinBounds(gameObject, dishesToSpawn.Count);

        for(int i = 0; i < dishesToSpawn.Count; i++) {
            DishController newDishController = dishControllerObjects[i].GetComponent<DishController>();
            newDishController.Init(dishesToSpawn[i], gridManager);
        }

        gridManager.FilterGrid();
        gridManager.DisplayGrid();
    }

    public void Teardown() {
        for (var i = 0; i < dishControllers.Count; i++) {
            Destroy(dishControllers[i].gameObject);
        }
        Destroy(gameObject);
    }

}
