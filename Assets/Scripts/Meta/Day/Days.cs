
using System;
using UnityEngine;

// I cannot believe that Unity makes you do this, just let me parse a list from json directly
[Serializable] 
public class Days
{
    [SerializeField]
    private Day[] daysArray;

    public Day[] DaysArray { get => daysArray; set => daysArray = value; }
}
