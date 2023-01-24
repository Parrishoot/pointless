using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayUIManager : MonoBehaviour
{

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI wakeUpText;
    public int textSpacing;

    private string startTag;
    private string endTag = "</mspace>";

    private Vector3 wakeUpTextLocalPositionStart;

    public void Start()
    {
        startTag = string.Format("<mspace=%i>", textSpacing);
        wakeUpTextLocalPositionStart = wakeUpText.rectTransform.localPosition;
    }

    public void SetDayText(int day)
    {
        dayText.SetText("DAY " + day.ToString());
    }

    public void SlideTextLeft()
    {
        wakeUpText.rectTransform.localPosition += (Vector3.left * textSpacing); 
    }

    public void ResetWakeUpText()
    {
        wakeUpText.rectTransform.localPosition = wakeUpTextLocalPositionStart;
    }

    public void EnableWakeUpText(string wakeUpTextString)
    {
        wakeUpText.SetText(startTag + wakeUpTextString + endTag);
        ResetWakeUpText();
        wakeUpText.enabled = true;
    }

    public void DisableWakeUpText()
    {
        wakeUpText.enabled = false;
    }
}
