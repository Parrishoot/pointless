using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishController : GridController
{

    public BoxCollider2D boxCollider2D;

    public Draggable draggable;

    private ChunkMeta chunkMeta;

    public void Init(ChunkMeta chunkMeta) {
        
        chunkMeta.NormalizePoints();
        
        this.chunkMeta = chunkMeta;
        this.gridBounds = chunkMeta.GetGridBounds();

        boxCollider2D.size = new Vector2(((float) this.gridBounds.x) / 2,
                                         ((float) this.gridBounds.y) / 2);
        boxCollider2D.offset = new Vector2(((float) this.gridBounds.x) / 4,
                                           ((float) this.gridBounds.y) / 4);

        draggable.SetOffset(-boxCollider2D.offset.x, -boxCollider2D.offset.y);

        DisplayGrid();
    }

    private void DisplayGrid() {
        foreach(Vector2Int point in chunkMeta.PointList) {
            SetSpace(point.x, convertToGridY(point.y), chunkMeta);
        }
    }
}
