using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesDebugState : MinigameState
{

    public DishesDebugState(GridManager gridManager, int gridSize) {
        this.gridManager = gridManager;
        this.gridSize = gridSize;
    }

    private GridManager gridManager;

    private int gridSize = 8;
    
    private int MAX_CHUNK_WIDTH = 3; 
    private float MERGE_CHANCE = .6f;

    private ChunkMeta[,] grid;

    private Grid gridObject;

    private GameObject gridObjectPrefab;

    public override void Setup()
    {
        InitGrid();

        base.Setup();
    }

    public override void Process()
    {
        base.Process();

        PrintGrid();
    }

    private void PrintGrid() {
        string currentString = "";
        for(int i = 0; i < gridSize; i++) {
            for(int j = 0; j < gridSize; j++) {
                currentString += grid[j,i].Id.ToString() + "\t";
            }
            currentString += "\n";
        }
        Debug.Log(currentString);
    }

    private void InitGrid() {

        grid = new ChunkMeta[gridSize, gridSize];

        int nextChunkID = 0;
        Color color = UnityEngine.Random.ColorHSV();

        for(int y = 0; y < gridSize; y++) {

            // Initialize a new chunk
            int chunkSize = 0;
            int currentChunkID = 0;

            for(int x = 0; x < gridSize; x++) {

                if(chunkSize == 0) {

                    chunkSize = Mathf.Min(Random.Range(1, MAX_CHUNK_WIDTH));
                    nextChunkID++;

                    if(y == 0 || Random.Range(0f, 1f) > MERGE_CHANCE) {
                        currentChunkID = nextChunkID;
                        color = UnityEngine.Random.ColorHSV();
                    }
                    else {

                        int mergeX = Mathf.Min(x + Random.Range(0, chunkSize - 1), gridSize - 1);

                        currentChunkID = grid[mergeX,y-1].Id;
                        color = grid[mergeX,y-1].Color;
                    }
                }

                grid[x,y] = new ChunkMeta(currentChunkID, color);
                gridManager.InitSpace(x, (gridSize - 1) - y, color);
                chunkSize--;
            }
        }
    }
}
