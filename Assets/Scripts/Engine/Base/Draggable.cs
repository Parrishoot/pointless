using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Follower))]
public class Draggable : MonoBehaviour
{
    public Vector2 offset;

    private Follower follower;

    public void Start() {
        follower = GetComponent<Follower>();
        follower.enabled = false;
    }

    public Vector2 GetMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown() {
        follower.enabled = true;
        follower.SetTarget(GetMousePosition() + offset);
    }

    private void OnMouseUp() {
        follower.enabled = false;
    }

    private void OnMouseDrag() {
        follower.SetTarget(GetMousePosition() + offset);
    }

    public void SetOffset(float x, float y) {
        offset = new Vector2(x, y);
    }
}
