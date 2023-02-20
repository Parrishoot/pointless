using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DishController : GridController, ISpawnable
{

    public Tilemap tileMap;

    public TileBase dishTileRule;

    public TilemapRenderer tilemapRenderer;

    public Draggable draggable;

    public Follower follower;

    private ChunkMeta chunkMeta;

    private GridManager gridManager;

    public void Init(ChunkMeta chunkMeta, GridManager gridManager) {
        
        chunkMeta.NormalizePoints();

        tileMap.ClearAllTiles();
        
        this.chunkMeta = chunkMeta;
        this.gridBounds = chunkMeta.GetGridBounds();
        this.gridManager = gridManager;

        draggable.SetOffset(-GetOffset().x, -GetOffset().y);

        DisplayGrid();
    }

    private void DisplayGrid() {
        foreach(Vector2Int point in chunkMeta.PointList) {
            tileMap.SetTile(new Vector3Int(point.x, convertToGridY(point.y), 0), dishTileRule);
        }
    }

    public void Place() {
        ResetOrder();
        Vector2 placeSpace = gridManager.PlaceDish(transform.position);
        if(!placeSpace.Equals(INVALID_SPACE)) {
            follower.SetTarget(placeSpace);
        }
    }

    public Vector2 GetOffset() {
        return new Vector2(((float) this.gridBounds.x) / 4,
                           ((float) this.gridBounds.y) / 4);
    }

    public void BringToFront() {
        tilemapRenderer.sortingOrder += 1;
    }

    public void ResetOrder() {
        tilemapRenderer.sortingOrder -= 1;
    }
}
