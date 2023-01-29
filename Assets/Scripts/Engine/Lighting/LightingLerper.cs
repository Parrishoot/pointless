using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingLerper : MonoBehaviour
{
    public float minIntensity = 0f;
    public float maxIntensity = 1f;

    public Light2D light2D;

    private DayManager dayManager;

    void Start() {
        dayManager = DayManager.GetInstance();
    }

    void Update() {
        light2D.intensity = dayManager.GetLerpedValueBasedOnTimeOfDay(minIntensity, maxIntensity);
    }
}
