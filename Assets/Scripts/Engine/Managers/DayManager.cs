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

    private DayMetaManager dayMetaManager;

    private PlayerMovementManager playerMovementManager;

    private DialogueUIManager dialogueUIManager;

    private int day = 0;

    private Queue<DialogueTrigger> dailyDialogueTriggers;

    // Start is called before the first frame update
    void Start()
    {
        dayMetaManager = DayMetaManager.GetInstance();
        playerMovementManager = PlayerMovementManager.GetInstance();
        dialogueUIManager = DialogueUIManager.GetInstance();
        InitializeDay();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.RUNNING:

                if(!dialogueUIManager.HasActiveDialogue()) {
                    timeLeftInDay -= Time.deltaTime;
                }

                if(dailyDialogueTriggers.Count != 0 && dailyDialogueTriggers.Peek().TriggerPercentage <= GetDayPercentage()) {
                    DialogueTrigger dialogueTrigger = dailyDialogueTriggers.Dequeue();
                    dialogueUIManager.CreateDialogue(dialogueTrigger.DialogueList);
                }

                if (timeLeftInDay <= 0)
                {
                    InitializeNewDay();
                }

                break;

            case STATE.WAKING_UP:
                if (dayUIManager.DayTransitionFinished())
                {
                   BeginNewDay();
                }
                else if(dayUIManager.CanResetDayTime()) {
                    ResetDayLength();
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
        Day newDay = dayMetaManager.GetDay(day);

        dayUIManager.BeginDayTransition(newDay.DayNumber, newDay.WakeUpText);
        
        state = STATE.WAKING_UP;
        playerMovementManager.DisableMovement();
    }

    private void InitializeNewDay() {
        day = (day + 1) % (dayMetaManager.GetDays().Count);
        InitializeDay();
    }

    private void BeginNewDay() {
        dailyDialogueTriggers = new Queue<DialogueTrigger>(dayMetaManager.GetDay(day).DialogueTriggers);
        state = STATE.RUNNING;
        playerMovementManager.EnableMovement();
    }

    public float GetDayPercentage() {
        return Mathf.Clamp((dayLength - timeLeftInDay) / dayLength, 0, 1);
    }

    public float GetLerpedValueBasedOnTimeOfDay(float minValue, float maxValue) {
        float timeOfDayPercentage = GetDayPercentage();
        return Mathf.Lerp(minValue, maxValue, timeOfDayPercentage);
    }
}
