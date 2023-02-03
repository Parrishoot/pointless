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

                timeLeftInDay -= Time.deltaTime;

                if(dailyDialogueTriggers.Count != 0 && dailyDialogueTriggers.Peek().TriggerPercentage < GetDayPercentage()) {
                    DialogueTrigger dialogueTrigger = dailyDialogueTriggers.Dequeue();
                    dialogueUIManager.CreateDialogue(dialogueTrigger.DialogueList);
                }

                if (timeLeftInDay <= 0)
                {
                    InitializeNewDay();
                }
                break;

            case STATE.WAKING_UP:
                if (InputManager.GetInstance().SpecificLetterKeyPressedThisFrame(wakeUpController.GetCurrentCharacter()))
                {
                    wakeUpController.Progress();

                    if(wakeUpController.IsFinished())
                    {
                        BeginNewDay();
                    }
                    else {
                        dayUIManager.Progress(wakeUpController.GetIndex());
                    }
                }
                else if(InputManager.GetInstance().AnyKeyPressedThisFrame())
                {
                    dayUIManager.ResetWakeUpText();
                    wakeUpController.Reset();
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

        dayUIManager.SetDayText(newDay.DayNumber);
        dailyDialogueTriggers = new Queue<DialogueTrigger>(newDay.DialogueTriggers);

        wakeUpController = new WakeUpController(dayMetaManager.GetDay(day).WakeUpText);
        dayUIManager.EnableWakeUpText(dayMetaManager.GetDay(day).WakeUpText);
        
        state = STATE.WAKING_UP;
        playerMovementManager.DisableMovement();
    }

    private void InitializeNewDay() {
        day = (day + 1) % (dayMetaManager.GetDays().Count);
        InitializeDay();
    }

    private void BeginNewDay() {
        dayUIManager.DisableWakeUpText();
        state = STATE.RUNNING;
        ResetDayLength();
        playerMovementManager.EnableMovement();
    }

    public float GetDayPercentage() {

        if(state != STATE.RUNNING) {
            return 0f;
        }

        return (dayLength - timeLeftInDay) / dayLength;
    }

    public float GetLerpedValueBasedOnTimeOfDay(float minValue, float maxValue) {
        float timeOfDayPercentage = GetDayPercentage();
        return Mathf.Lerp(minValue, maxValue, timeOfDayPercentage);
    }
}
