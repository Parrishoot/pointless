using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayMetaManager : Singleton<DayMetaManager>
{
    public List<Day> dayMetaList;

    public List<Day> GetDays() {
        return dayMetaList;
    }

    public Day GetDay(int index) {
        return dayMetaList[index];
    }
}
