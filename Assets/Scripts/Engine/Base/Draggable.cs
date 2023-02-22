
using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    public Vector2 offset;

    public UnityEvent onMouseDownCallback;

    public UnityEvent onMouseUpCallback;

    public Follower follower;

    private Vector2 startDragLocation;

    public Vector2 GetMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown() {
        startDragLocation = transform.position;
        onMouseDownCallback?.Invoke();
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

    public Vector2 GetLastDragStartLocation() {
        return startDragLocation;
    }
}
