using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : GridController
{
    public int MAX_CHUNK_WIDTH = 4;

    public int desiredPieceCount = 8;

    public int desiredWallCount = 4;

    public BoxCollider2D gridCollider;

    public Color openColor;

    public Color wallColor;

    private List<ChunkMeta> chunkMetaList;

    public ChunkMeta[,] chunkGrid;

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

        // Create some walls
        CreateWalls();

        gridCollider.size = new Vector2(this.gridBounds.x * gridComponent.cellSize.x,
                                        this.gridBounds.y * gridComponent.cellSize.y);
        gridCollider.offset = gridCollider.size / 2;
    }

    public void FilterGrid() {
        for(int x = 0; x < gridBounds.x; x++) {
            for(int y = 0; y < gridBounds.y; y++) {
                if(!chunkGrid[x,y].IsWall()) {
                    chunkGrid[x,y] = null;
                }
            }
        }
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

    public void DisplayGrid() {
        for(int y = 0; y < gridBounds.y; y++) {
            for(int x = 0; x < gridBounds.x; x++) {
                Color spaceColor = chunkGrid[x,y] == null ? openColor : wallColor;
                SetSpace(x, convertToGridY(y), spaceColor);
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
                    currentChunk = GenerateChunk();
                }

                chunkGrid[x,y] = currentChunk;
                currentChunk.AddPoint(new Vector2Int(x, y));
            }
        }
    }

    private void MergeChunks() {

        // Merge chunks until we've hit the right amount of blocks
        while(chunkMetaList.Count > (desiredPieceCount + desiredWallCount)) {

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

    public void CreateWalls() {
        
        List<ChunkMeta> edgeChunks = chunkMetaList.FindAll(chunk => chunk.OnEdge(gridBounds));

        int wallsLeftToCreate = Mathf.Min(desiredWallCount, edgeChunks.Count);

        while(desiredWallCount > 0) {

            int wallIndex = Random.Range(0, edgeChunks.Count);
            
            if(!edgeChunks[wallIndex].IsWall()) {
                desiredWallCount--;
                edgeChunks[wallIndex].SetWall(wallColor);
            }
        }
    }

    public List<ChunkMeta> GetChunks() {
        return chunkMetaList;
    }

    public List<ChunkMeta> GetChunksOfType(ChunkMeta.ChunkType chunkType) {
        return chunkMetaList.FindAll(chunk => chunk.ChunkTypeValue == chunkType);
    }

    public Vector2 PlaceDish(ChunkMeta chunkMeta, Vector2 dishGlobalPosition) {

        Vector2 positionDifference = dishGlobalPosition - new Vector2(transform.position.x, transform.position.y);

        Vector2Int placeSpace = FindClosestGridSpace(positionDifference);

        if(placeSpace == INVALID_SPACE) {
            return placeSpace;
        }

        bool clashingPoints = chunkMeta.PointList.Exists(point => !IsValidSpace(placeSpace.x + point.x, placeSpace.y + point.y));
        List<Vector2Int> clashingPointsList = chunkMeta.PointList.FindAll(point => !IsValidSpace(placeSpace.x + point.x, placeSpace.y + point.y));
        if(clashingPoints) {
            return INVALID_SPACE;
        }

        foreach(Vector2Int point in chunkMeta.PointList) {
            chunkGrid[placeSpace.x + point.x, placeSpace.y + point.y] = chunkMeta;
        }

        Vector2 cellCenter = gridComponent.GetCellCenterWorld(new Vector3Int(placeSpace.x, placeSpace.y, 0));
        return cellCenter - new Vector2(gridComponent.cellSize.x / 2, gridComponent.cellSize.y / 2);
    }

    public void RemoveDish(ChunkMeta chunkMeta) {
        for(int y = 0; y < gridBounds.x; y++) {
            for(int x = 0; x < gridBounds.x; x++) {
                if(chunkGrid[x,y] != null && chunkGrid[x,y] == chunkMeta) {
                    chunkGrid[x,y] = null;
                }
            }
        }
    }

    private bool IsValidSpace(int x, int y) {
        return x < this.gridBounds.x &&
               y < this.gridBounds.y &&
               chunkGrid[x,y] == null;
    }
}
