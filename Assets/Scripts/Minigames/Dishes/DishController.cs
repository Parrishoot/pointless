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

    public float rotateTime;

    public Rotator rotator;

    private ChunkMeta chunkMeta;

    private GridManager gridManager;

    private bool held = false;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R) && held) {
            RotateCounterClockwise();
        }
        else if(Input.GetKeyDown(KeyCode.T) && held) {
            RotateClockwise();
        }
    }

    public void Init(ChunkMeta chunkMeta, GridManager gridManager) {
        
        chunkMeta.NormalizePoints();
        
        this.chunkMeta = chunkMeta;
        this.gridManager = gridManager;

        for(int i = 0; i < Random.Range(0, 3); i++) {
            chunkMeta.RotateClockwise();
        }

        ResetDish();

        DisplayGrid();
    }

    private void ResetDish() { 
        this.gridBounds = chunkMeta.GetGridBounds();
        tileMap.transform.localPosition = new Vector2(-GetOffset().x, -GetOffset().y);
    }

    private void DisplayGrid() {

        tileMap.ClearAllTiles();

        foreach(Vector2Int point in chunkMeta.PointList) {
            tileMap.SetTile(new Vector3Int(point.x, convertToGridY(point.y), 0), dishTileRule);
        }
    }

    private void RotateClockwise() {
        chunkMeta.RotateClockwise();
        rotator.BeginRotation(new Vector3(0, 0, 90), rotateTime);
        ResetDish();
    }

    private void RotateCounterClockwise() {
        chunkMeta.RotateCounterClockwise();
        rotator.BeginRotation(new Vector3(0, 0, -90), rotateTime);
        ResetDish();
    }

    public void Place() {
        held = false;
        ResetOrder();
        Vector2 placeSpace = gridManager.PlaceDish(tileMap.transform.position);
        if(!placeSpace.Equals(INVALID_SPACE)) {
            follower.SetTarget(placeSpace + GetOffset());
        }
    }

    public Vector2 GetOffset() {
        return new Vector2(((float) this.gridBounds.x) / 4,
                           ((float) this.gridBounds.y) / 4);
    }

    public void Hold() {
        held = true;
        tilemapRenderer.sortingOrder += 1;

    }

    public void ResetOrder() {
        tilemapRenderer.sortingOrder -= 1;
    }
}
