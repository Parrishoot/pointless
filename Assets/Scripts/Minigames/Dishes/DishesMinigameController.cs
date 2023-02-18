using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesMinigameController : MinigameController<DishesMinigameController>
{
    
    public GridManager gridManager;

    public DishesGameState debugState;

    public GameObject dishPreb;

    public List<DishController> dishControllers;

    public override void Start()
    {
        debugState = new DishesGameState();

        currentState = debugState;

        base.Start();
    }

    public void SpawnDish(ChunkMeta chunkMeta) {
        DishController dishController = Instantiate(dishPreb, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<DishController>();
        dishController.Init(chunkMeta);

        dishControllers.Add(dishController);
    }

    public override void Teardown() {
        for (var i = 0; i < dishControllers.Count; i++) {
            Destroy(dishControllers[i].gameObject);
        }
    }
}
