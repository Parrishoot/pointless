using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Follower))]
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

    private Follower follower;

    private Dictionary<POSITION, Transform> transformDict = new Dictionary<POSITION, Transform>();

    // Start is called before the first frame update
    void Start()
    {
        follower = GetComponent<Follower>();

        follower.SetTarget(centerTransform);

        transformDict.Add(POSITION.LEFT, leftTransform);
        transformDict.Add(POSITION.RIGHT, rightTransform);
        transformDict.Add(POSITION.CENTER, centerTransform);

    }

    public void SetCameraPosition(POSITION position)
    {
        follower.SetTarget(transformDict[position]);
    }
}
