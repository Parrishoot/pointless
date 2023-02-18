using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Follower))]
public class Draggable : MonoBehaviour
{
    public Vector2 offset;

    public UnityEvent onMouseDownCallback;

    public UnityEvent onMouseUpCallback;

    private Follower follower;

    public void Start() {
        follower = GetComponent<Follower>();
    }

    public Vector2 GetMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown() {
        onMouseDownCallback.Invoke();
        follower.SetTarget(GetMousePosition() + offset);
    }

    private void OnMouseUp() {
        onMouseUpCallback?.Invoke();
    }

    private void OnMouseDrag() {
        follower.SetTarget(GetMousePosition() + offset);
    }

    public void SetOffset(float x, float y) {
        offset = new Vector2(x, y);
    }
}
