using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpController
{
    private string wakeUpString;
    private int index;

    public WakeUpController(string wakeUpString)
    {
        this.wakeUpString = wakeUpString;
        Reset();
    }

    public void Reset()
    {
        index = 0;
    }

    public string GetCurrentCharacter()
    {
        return wakeUpString[index].ToString();
    }

    public bool IsFinished()
    {
        return index == wakeUpString.Length;
    }

    public void Progress()
    {
        index++;
    }
}
