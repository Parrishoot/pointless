using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float followSpeed;

    private Vector2 targetPosition;

    private bool beginFollow = false;

    public void SetTarget(Transform transform) {
        beginFollow = true;
        targetPosition = transform.position;
    }

    public void SetTarget(Vector2 newPositon) {
        beginFollow = true;
        targetPosition = newPositon;
    }

    // Update is called once per frame
    void Update()
    {
        if(beginFollow) {
            float currentX = transform.position.x;
            float currentY = transform.position.y;

            float targetX = targetPosition.x;
            float targetY = targetPosition.y;

            gameObject.transform.position += (new Vector3((targetX - currentX) * followSpeed * Time.deltaTime,
                                                          (targetY - currentY) * followSpeed * Time.deltaTime,
                                                           0));
        }

    }
}
