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

    private DayMetaManager dayMetaManager;

    private PlayerMovement playerMovement;

    private int day = 0;

    // Start is called before the first frame update
    void Start()
    {
        dayMetaManager = DayMetaManager.GetInstance();
        playerMovement = PlayerMovement.GetInstance();
        InitializeDay();
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
                    InitializeNewDay();
                }
                break;

            case STATE.WAKING_UP:
                if (InputManager.GetInstance().SpecificLetterKeyPressedThisFrame(wakeUpController.GetCurrentCharacter()))
                {
                    dayUIManager.SlideTextLeft();
                    wakeUpController.Progress();
                }
                else if(InputManager.GetInstance().AnyKeyPressedThisFrame())
                {
                    dayUIManager.ResetWakeUpText();
                    wakeUpController.Reset();
                }

                if(wakeUpController.IsFinished())
                {
                    BeginNewDay();
                }
                break;

        }
    }

    public bool IsWakingUp() {
        return state == STATE.WAKING_UP;
    }

    private void ResetDayLength()
    {
        timeLeftInDay = dayLength;
    }

    private void InitializeDay() {
        dayUIManager.SetDayText(dayMetaManager.GetDay(day).DayNumber);
        wakeUpController = new WakeUpController(dayMetaManager.GetDay(day).WakeUpText);
        dayUIManager.EnableWakeUpText(dayMetaManager.GetDay(day).WakeUpText);
        
        state = STATE.WAKING_UP;
        playerMovement.Disable();
    }

    private void InitializeNewDay() {
        day = (day + 1) % (dayMetaManager.GetDays().Count);
        InitializeDay();
    }

    private void BeginNewDay() {
        dayUIManager.DisableWakeUpText();
        state = STATE.RUNNING;
        ResetDayLength();
        playerMovement.Enable();
    }
}
