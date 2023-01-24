using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : Singleton<DayManager>
{

    public enum STATE
    {
        IDLE,
        WAKING_UP,
        RUNNING
    }

    public float dayLength;

    public DayUIManager dayUIManager;

    private STATE state;

    private float timeLeftInDay;

    private WakeUpController wakeUpController;

    private int day;

    // Start is called before the first frame update
    void Start()
    {
        timeLeftInDay = dayLength;
        state = STATE.RUNNING;
        dayUIManager.DisableWakeUpText();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.RUNNING:
                timeLeftInDay -= Time.deltaTime;
                if (timeLeftInDay <= 0)
                {
                    Debug.Log("Waking up!");

                    day++;
                    dayUIManager.SetDayText(day);
                    wakeUpController = new WakeUpController("WAKEUP");
                    dayUIManager.EnableWakeUpText("WAKEUP");
                    state = STATE.WAKING_UP;
                }
                break;

            case STATE.WAKING_UP:
                if (InputManager.GetInstance().SpecificLetterKeyPressedThisFrame(wakeUpController.GetCurrentCharacter()))
                {
                    Debug.Log("Correct!");
                    dayUIManager.SlideTextLeft();
                    wakeUpController.Progress();
                }
                else if(InputManager.GetInstance().AnyKeyPressedThisFrame())
                {
                    Debug.Log("Incorrect!");
                    Debug.Log(wakeUpController.GetCurrentCharacter().ToLower());
                    dayUIManager.ResetWakeUpText();
                    wakeUpController.Reset();
                }

                if(wakeUpController.IsFinished())
                {
                    dayUIManager.DisableWakeUpText();
                    state = STATE.RUNNING;
                    ResetDayLength();
                }
                break;

        }
        {
            
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            
        }
    }

    private void ResetDayLength()
    {
        timeLeftInDay = dayLength;
    }
}
