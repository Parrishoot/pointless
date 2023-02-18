using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesMinigameController : MinigameController<DishesMinigameController>
{
    
    public GridManager gridManager;

    public DishesGameState debugState;

    public GameObject dishPreb;

    public List<DishController> dishControllers;

    public Spawner dishSpawner;

    public override void Start()
    {
        debugState = new DishesGameState();

        currentState = debugState;

        base.Start();
    }

    public override void Teardown() {
        for (var i = 0; i < dishControllers.Count; i++) {
            Destroy(dishControllers[i].gameObject);
        }
    }

}
