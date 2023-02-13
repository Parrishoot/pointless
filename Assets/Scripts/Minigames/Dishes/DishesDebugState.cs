using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesDebugState : MinigameState
{

    private int GRID_SIZE = 8;
    
    private int MAX_CHUNK_WIDTH = 4; 
    private float MERGE_CHANCE = .5f;

    private ChunkMeta[,] grid;

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
        for(int i = 0; i < GRID_SIZE; i++) {
            for(int j = 0; j < GRID_SIZE; j++) {
                currentString += grid[j,i].Id.ToString() + "\t";
            }
            currentString += "\n";
        }
        Debug.Log(currentString);
    }

    private void InitGrid() {

        grid = new ChunkMeta[GRID_SIZE, GRID_SIZE];

        int nextChunkID = 0;

        for(int y = 0; y < GRID_SIZE; y++) {

            // Initialize a new chunk
            int chunkSize = 0;
            int currentChunkID = 0;

            for(int x = 0; x < GRID_SIZE; x++) {

                if(chunkSize == 0) {
                    chunkSize = Mathf.Min(Random.Range(1, MAX_CHUNK_WIDTH));
                    nextChunkID++;
                    currentChunkID = y == 0 || Random.Range(0f, 1f) < MERGE_CHANCE ? nextChunkID : grid[x,y-1].Id;
                }


                grid[x,y] = new ChunkMeta(currentChunkID);

                chunkSize--;
            }
        }
    }
}
