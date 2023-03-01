using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DishController : GridController, ISpawnable
{

    public Tilemap tileMap;

    public TileBase dishTileRule;

    public TilemapRenderer tilemapRenderer;

    public DishSnapBackController dishSnapBackController;

    public Follower follower;

    public float rotateTime;

    public Rotator rotator;

    public Transform placePoint;

    private ChunkMeta chunkMeta;

    private GridManager gridManager;

    private bool held = false;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R) && held) {
            RotateClockwise();
        }
        else if(Input.GetKeyDown(KeyCode.T) && held) {
            RotateCounterClockwise();
        }
    }

    public void Init(ChunkMeta chunkMeta, GridManager gridManager) {
        
        chunkMeta.NormalizePoints();
        
        this.chunkMeta = chunkMeta;
        this.gridManager = gridManager;

        for(int i = 0; i < Random.Range(0, 3); i++) {
            chunkMeta.RotateClockwise();
        }

        tileMap.transform.localPosition = new Vector2(-GetOffset().x, -GetOffset().y);

        ResetPlacePoint();

        DisplayGrid();
    }

    private void ResetPlacePoint(){

        int targetRotation = (int) rotator.GetTargetRotation().z;
        if(targetRotation < 0) {
            targetRotation = 360 - Mathf.Abs(targetRotation % 360);
        }
        targetRotation = targetRotation % 360;

        switch(targetRotation) {
            case 0:
                placePoint.transform.localPosition = new Vector2(-GetOffset().x, -GetOffset().y);
                break;
            case 90:
                placePoint.transform.localPosition = new Vector2(-GetOffset().y, GetOffset().x);
                break;
            case 180:
                placePoint.transform.localPosition = new Vector2(GetOffset().x, GetOffset().y);
                break;
            case 270:
                placePoint.transform.localPosition = new Vector2(GetOffset().y, -GetOffset().x);
                break;
        }

    }
    private void DisplayGrid() {

        tileMap.ClearAllTiles();

        foreach(Vector2Int point in chunkMeta.PointList) {
            tileMap.SetTile(new Vector3Int(point.x, point.y, 0), dishTileRule);
        }
    }

    private void RotateClockwise() {
        chunkMeta.RotateClockwise();
        rotator.BeginRotation(new Vector3(0, 0, 90), rotateTime);
        ResetPlacePoint();
    }

    private void RotateCounterClockwise() {
        chunkMeta.RotateCounterClockwise();
        rotator.BeginRotation(new Vector3(0, 0, -90), rotateTime);
        ResetPlacePoint();
    }

    public void Place() {
        held = false;
        ResetOrder();
        Vector2 placeSpace = gridManager.PlaceDish(chunkMeta, placePoint.transform.position);
        if(!placeSpace.Equals(INVALID_SPACE)) {
            follower.SetTarget(placeSpace + (Vector2) (transform.position - placePoint.transform.position));
        }
        else if(dishSnapBackController.TouchingGrid()) {
            follower.SetTarget(dishSnapBackController.GetSnapbackLocation() + (Vector2) (transform.position - dishSnapBackController.transform.position));
        }
    }

    public Vector2 GetOffset() {
        Vector2 dishGridBounds = this.chunkMeta.GetGridBounds();
        return new Vector2(((float) dishGridBounds.x / 2) * gridComponent.cellSize.x,
                           ((float) dishGridBounds.y / 2) * gridComponent.cellSize.y);
    }

    public void Hold() {
        held = true;
        gridManager.RemoveDish(this.chunkMeta);
        tilemapRenderer.sortingOrder += 1;

    }

    public void ResetOrder() {
        tilemapRenderer.sortingOrder -= 1;
    }

    public void BeginEndTransition(Transform newParent) {
        transform.SetParent(newParent, true);
        follower.enabled = false;
        tilemapRenderer.sortingLayerName = "Dishes Underneath";
    }
}
