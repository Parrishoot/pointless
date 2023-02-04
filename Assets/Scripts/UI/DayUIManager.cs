using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class DayUIManager : MonoBehaviour
{
    public DayMaskAnimationController dayMaskAnimationController;

    public WakeUpTextUIManager wakeUpTextUIManager;

    public TextMeshProUGUI dayTextMesh;

    public float dayTransitionWaitTime;
    
    private enum DAY_TRANSITION_STATE {
        IDLE,
        SHOW_DAY,
        WAKING_UP
    }

    private DAY_TRANSITION_STATE dayTransitionState;

    private string nextWakeUpText;

    public Timer timer;

    public void Update() {

        switch(dayTransitionState) {

            case DAY_TRANSITION_STATE.IDLE:
                break;

            case DAY_TRANSITION_STATE.SHOW_DAY:
                if(timer.IsFinished()) {
                    dayTransitionState = DAY_TRANSITION_STATE.WAKING_UP;
                    dayMaskAnimationController.Hide();
                    wakeUpTextUIManager.InitializeWakeUpText(nextWakeUpText);
                }
                break;

            case DAY_TRANSITION_STATE.WAKING_UP:
                if(!wakeUpTextUIManager.HasActiveWakeUpText()) {
                    dayTransitionState = DAY_TRANSITION_STATE.IDLE;
                }
                break;

        }

    }

    public bool DayTransitionFinished() {
        // TODO
        return dayTransitionState == DAY_TRANSITION_STATE.IDLE;
    }

    public void BeginDayTransition(int dayNumber, string wakeUpText) {
        dayTransitionState = DAY_TRANSITION_STATE.SHOW_DAY;
        nextWakeUpText = wakeUpText;
        dayTextMesh.SetText("DAY " + dayNumber.ToString());
        dayMaskAnimationController.Show();
        timer.SetTimer(dayTransitionWaitTime);
    }

    public bool CanResetDayTime() {
        return dayTransitionState == DAY_TRANSITION_STATE.WAKING_UP;
    }

}
