using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesMinigameController : MonoBehaviour, IMinigameController
{
    public Transform gridParent;

    public Animator animator;

    public GridManager gridManager;

    public GameObject dishPreb;

    public List<DishController> dishControllers;

    public Spawner dishSpawner;

    public Transform dishSpawnLocation;

    public new ParticleSystem particleSystem;

    private bool solved = false;

    public bool IsFinished()
    {
        return solved && particleSystem.isStopped;
    }

    public void Start()
    {
        gridManager.InitGrid();

        List<ChunkMeta> dishesToSpawn = gridManager.GetChunksOfType(ChunkMeta.ChunkType.DISH);
        List<GameObject> dishControllerObjects = dishSpawner.Spawn(dishSpawnLocation.transform.position, gameObject, dishesToSpawn.Count);

        for(int i = 0; i < dishesToSpawn.Count; i++) {
            DishController newDishController = dishControllerObjects[i].GetComponent<DishController>();
            newDishController.Init(dishesToSpawn[i], gridManager);
            dishControllers.Add(newDishController);
        }

        gridManager.FilterGrid();
        gridManager.DisplayGrid();
    }

    private void Update() {

        if(gridManager.Solved()) {
            
            if(!particleSystem.isPlaying && !solved) {
                EndMinigame();
            }
        }

    }

    public void Teardown() {
        for (var i = 0; i < dishControllers.Count; i++) {
            Destroy(dishControllers[i].gameObject);
        }
        Destroy(gameObject);
    }

    public void EndMinigame() {
        foreach(DishController dishController in dishControllers) {
            dishController.BeginEndTransition(gridParent);
        }
        particleSystem.Play();
        solved = true;
        animator.Play("DishDespawn");
    }

}
