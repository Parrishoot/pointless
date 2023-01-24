using System;
using UnityEngine;

[Serializable] 
public class Day
{
    [SerializeField]
    private int dayNumber;

    [SerializeField]
    private string wakeUpText = "WAKEUP";

    public int DayNumber { get => dayNumber; set => dayNumber = value; }
    public string WakeUpText { get => wakeUpText; set => wakeUpText = value; }
}
