using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Grid gridComponent;

    public GameObject gridObjectPrefab;

    public Vector2Int gridBounds;

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
}
