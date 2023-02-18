using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Grid gridComponent;

    public GameObject gridObjectPrefab;

    public Vector2Int gridBounds;

    public static Vector2Int INVALID_SPACE = new Vector2Int(-1, -1);

    public void InitPosition() {
        gridComponent.transform.localPosition = new Vector2(-gridBounds.x / 4, -gridBounds.y / 4);
    }

    public void SetSpace(int x, int y, ChunkMeta chunkMeta) {
            GameObject newGameObject = Instantiate(gridObjectPrefab);
            newGameObject.transform.SetParent(gameObject.transform, false);
            newGameObject.transform.localPosition = gridComponent.GetCellCenterLocal(new Vector3Int(x, y, 0));
            newGameObject.GetComponent<SpriteRenderer>().color = chunkMeta.Color;
    }

    public int convertToGridY(int y) {
        return (gridBounds.y - 1) - y;
    }

    protected bool WithinGridBounds(Vector2 position) {
        return position.x >= 0 && 
               position.y >= 0 &&
               position.x <= (gridBounds.x * gridComponent.cellSize.x) && 
               position.y <= (gridBounds.y * gridComponent.cellSize.y);
    }

    public Vector2Int FindClosestGridSpace(Vector2 position) {

        if(!WithinGridBounds(position)) {
            return INVALID_SPACE;
        }

        // TODO: Fix this logic
        // Vector2 roundedPosition = new Vector2(Mathf.RoundToInt(position.x * (1 / gridComponent.cellSize.x)) / (1 / gridComponent.cellSize.x),
        //                                       Mathf.RoundToInt(position.y * (1 / gridComponent.cellSize.y)) / (1 / gridComponent.cellSize.y));
        Vector2 roundedPosition = position;

        return new Vector2Int((int)(roundedPosition.x / gridComponent.cellSize.x),
                              (int)(roundedPosition.y / gridComponent.cellSize.y));

    }
}