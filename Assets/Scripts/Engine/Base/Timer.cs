using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private float currentWaitTime = 0;

    private float totalWaitTime = 0f;

    public bool IsFinished() {
        return currentWaitTime <= 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaitTime > 0) {
            currentWaitTime -= Time.deltaTime;
        }
    }

    public void SetTimer(float waitTime) {
        totalWaitTime = waitTime;
        currentWaitTime = waitTime;
    }

    public float GetCompletionPercentage() {
        return (totalWaitTime - currentWaitTime) / totalWaitTime;
    }
}
