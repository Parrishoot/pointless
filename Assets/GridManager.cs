using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid grid; 

    public GameObject gridObjectPrefab;

    public void InitSpace(int x, int y, Color color) {
            GameObject newGameObject = Instantiate(gridObjectPrefab);
            newGameObject.transform.SetParent(gameObject.transform, false);
            newGameObject.transform.localPosition = grid.GetCellCenterLocal(new Vector3Int(x, y, 0));
            newGameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
