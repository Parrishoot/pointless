using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class DialogueTrigger
{
    [SerializeField]
    private float triggerPercentage;

    [SerializeField]
    private Dialogue[] dialogueList;

    public float TriggerPercentage { get => triggerPercentage; set => triggerPercentage = value; }
    public Dialogue[] DialogueList { get => dialogueList; set => dialogueList = value; }
}
