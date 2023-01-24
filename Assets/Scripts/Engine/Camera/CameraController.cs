using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{

    public enum POSITION
    {
        LEFT,
        RIGHT,
        CENTER
    }

    public Transform leftTransform;
    public Transform rightTransform;
    public Transform centerTransform;

    public float transitionSpeed;

    private Transform targetTransform;
    private Dictionary<POSITION, Transform> transformDict = new Dictionary<POSITION, Transform>();

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = centerTransform;

        transformDict.Add(POSITION.LEFT, leftTransform);
        transformDict.Add(POSITION.RIGHT, rightTransform);
        transformDict.Add(POSITION.CENTER, centerTransform);

    }

    // Update is called once per frame
    void Update()
    {

        float currentX = transform.position.x;
        float currentY = transform.position.y;

        float targetX = targetTransform.position.x;
        float targetY = targetTransform.position.y;

        gameObject.transform.Translate(new Vector3((targetX - currentX) * transitionSpeed * Time.deltaTime,
                                                   (targetY - currentY) * transitionSpeed * Time.deltaTime,
                                                    0));
    }

    public void SetCameraPosition(POSITION position)
    {
        targetTransform = transformDict[position];
    }
}
