using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DayMetaManager : Singleton<DayMetaManager>
{
    private List<Day> dayMetaList;

    public override void Awake() {
        base.Awake();

        LoadDays();
    }

    public void LoadDays() {
        TextAsset daysFile = Resources.Load<TextAsset>("Days");
        string daysJson = daysFile.ToString();
        dayMetaList = Enumerable.ToList<Day>(JsonUtility.FromJson<Days>(daysJson).DaysArray);
    }

    public List<Day> GetDays() {
        return dayMetaList;
    }

    public Day GetDay(int index) {
        return dayMetaList[index];
    }
}
