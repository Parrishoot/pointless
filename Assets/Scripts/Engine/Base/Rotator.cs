using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class Rotator : MonoBehaviour
{

    public AnimationCurve rotationCurve;

    private Timer timer;

    private Vector3 targetRotation;

    private Vector3 beginningRotation;

    private Vector3 pivotPoint;

    public void Start() {
        timer = GetComponent<Timer>();
        targetRotation = transform.rotation.eulerAngles;
    }

    public void BeginRotation(Vector3 rotationAmount, float rotateTime) {
        beginningRotation = targetRotation;
        targetRotation = targetRotation + rotationAmount;
        timer.SetTimer(rotateTime);
    }

    public void Update() {
        if(!timer.IsFinished()) {
            Debug.Log("Rotating!");
            transform.eulerAngles = Vector3.Lerp(beginningRotation, targetRotation, rotationCurve.Evaluate(timer.GetCompletionPercentage()));
        }
    }
}
