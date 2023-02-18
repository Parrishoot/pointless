using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : GridController
{
    private int MAX_CHUNK_WIDTH = 4; 

    public int desiredPieceCount = 12;

    private List<ChunkMeta> chunkMetaList;

    private ChunkMeta[,] chunkGrid;

    public void Start() {
        InitPosition();
    }

    public void InitGrid() {

        chunkGrid = new ChunkMeta[gridBounds.x, gridBounds.y];
        chunkMetaList = new List<ChunkMeta>();

        // Create a bunch of one-row chunks
        CreateBasicGrid();

        // Merge those chunks together
        MergeChunks();

        // TODO: Remove x chunks and set them as immovable

        // Show the grid
        DisplayGrid();
    }

    private ChunkMeta GenerateChunk() {
        ChunkMeta newChunk = ChunkMeta.GenerateChunk();
        chunkMetaList.Add(newChunk);
        return newChunk;
    }

    private void Merge(ChunkMeta superChunk, ChunkMeta eatenChunk) {


        foreach(Vector2Int point in eatenChunk.PointList) {
            chunkGrid[point.x, point.y] = superChunk;
        }

        superChunk.MergeChunk(eatenChunk);
        chunkMetaList.Remove(eatenChunk);
    }

    private void DisplayGrid() {
        for(int x = 0; x < gridBounds.x; x++) {
            for(int y = 0; y < gridBounds.y; y++) {
                SetSpace(x, convertToGridY(y), chunkGrid[x, y]);
            }
        }
    }

    private void CreateBasicGrid() {
        for(int y = 0; y < gridBounds.y; y++) {

            int remainingChunkSize = Random.Range(1, MAX_CHUNK_WIDTH);

            // Initialize a new chunk
            ChunkMeta currentChunk = GenerateChunk();

            for(int x = 0; x < gridBounds.x; x++) {

                if(currentChunk.PointList.Count >= remainingChunkSize) {

                    remainingChunkSize = Mathf.Min(Random.Range(1, MAX_CHUNK_WIDTH));

                    // if(y == 0 || Random.Range(0f, 1f) > MERGE_CHANCE) {
                    currentChunk = GenerateChunk();
                    // }
                    // else {

                       // int mergeX = Mathf.Min(x + Random.Range(0, remainingChunkSize - 1), gridSize - 1);
                       // currentChunk = grid[mergeX, y-1];
                   // }
                }

                chunkGrid[x,y] = currentChunk;
                currentChunk.AddPoint(new Vector2Int(x, y));
            }
        }
    }

    private void MergeChunks() {

        // Merge chunks until we've hit the right amount of blocks
        while(chunkMetaList.Count > desiredPieceCount) {

            // Find a random point on the smallest available chunk
            ChunkMeta chunkToMerge = chunkMetaList.OrderBy(x => x.PointList.Count).ToList()[0];
            Vector2Int mergePoint = chunkToMerge.PointList.ElementAt((int) Random.Range(0, chunkToMerge.PointList.Count));

            // There is definitely a better way to do this but I'm tired
            int[] directions = {1, -1};
            int directionListRandomizer = (int) Random.Range(0, 1);
            List<int> directionsToCheck = new List<int>(new int[]{directions[directionListRandomizer % 2], directions[directionListRandomizer + 1 % 2]});

            // Go up until you hit a new chunk
            // If you hit the edge of the grid, try the other direction
            foreach(int direction in directionsToCheck) {

                int yToCheck = (int) mergePoint.y;
                ChunkMeta otherChunk;
                bool merged = false;

                // Make sure you're in the bounds of the grid
                while(!(yToCheck < 0 || yToCheck > gridBounds.y - 1)) {
                    
                    otherChunk = chunkGrid[(int) mergePoint.x, yToCheck];
                    
                    // If the chunk hit is not the same as the current chunk,
                    // merge this small chunk into that one
                    if(!otherChunk.Equals(chunkToMerge)) {
                        Merge(otherChunk, chunkToMerge);
                        merged = true;
                        break;
                    }
                    // Otherwise, keep moving in this direction
                    else {
                        yToCheck += (1 * direction);
                    }
                }

                // Once we've merge the chunk, we can stop checking for this piece
                if(merged) {
                    break;
                }
            }
        }
    }

    public List<ChunkMeta> GetChunks() {
        return chunkMetaList;
    }
}
