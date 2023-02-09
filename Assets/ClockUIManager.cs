using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUIManager : MonoBehaviour
{

    public Transform hourHandTransform;
    public Transform minuteHandTransform;

    private DayManager dayManager;

    private int START_TIME = 8;

    void Start() {
        dayManager = DayManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        minuteHandTransform.eulerAngles = new Vector3(0, 0, GetMinuteHandRotation());
        hourHandTransform.eulerAngles = new Vector3(0, 0, GetHourHandRotation());
    }

    public float GetMinuteHandRotation() {

        return -(dayManager.GetLerpedValueBasedOnTimeOfDay(START_TIME * 60, 60 * 24) % 60) / 60 * 360;

    }

    public float GetHourHandRotation() {

        return -(dayManager.GetLerpedValueBasedOnTimeOfDay(START_TIME, 24) % 12) / 12 * 360;

    }
}
