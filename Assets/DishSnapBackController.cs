using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishSnapBackController : MonoBehaviour
{

    public Draggable draggable;

    private bool touchingGrid = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "DishGrid") {
            touchingGrid = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "DishGrid") {
            touchingGrid = false;
        }
    }

    public bool TouchingGrid() {
        return touchingGrid;
    }

    public Vector2 GetSnapbackLocation() {
        return draggable.GetLastDragStartLocation();
    }
}
