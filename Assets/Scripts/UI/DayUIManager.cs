using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayUIManager : MonoBehaviour
{

    public TextMeshProUGUI dayText;
    public TMP_Text wakeUpText;
    public int textSpacing;

    public float flashSpeed = 1f;

    public float wakeUpTextSlideTime = .1f;

    private float remainingWakeUpTextSlideTime = 0f;

    private Vector3 wakeUpTextPositionStart;

    private Vector3 wakeUpTextInitialLocalDayPosition;

    private Vector3 lerpTargetPosition;

    private Vector3 lerpStartPosition;

    private int currentIndex = 0;

    public void Start()
    {
        wakeUpTextPositionStart = wakeUpText.rectTransform.position;
    }

    public void Update() {
        if(wakeUpText.enabled) {

            if(remainingWakeUpTextSlideTime >= 0) {
                remainingWakeUpTextSlideTime -= Time.deltaTime;
                float time = Mathf.Clamp((wakeUpTextSlideTime - remainingWakeUpTextSlideTime) / wakeUpTextSlideTime, 0f, 1f);
                wakeUpText.rectTransform.localPosition = Vector3.Lerp(lerpStartPosition, lerpTargetPosition, time);
            }
            else if(lerpTargetPosition != wakeUpText.rectTransform.localPosition) {
                wakeUpText.rectTransform.localPosition = lerpTargetPosition;
            }

            SetCharacterAlpha(currentIndex, Mathf.PingPong(Time.time * flashSpeed, 1));
        }
    }

    public void SetDayText(int day)
    {
        dayText.SetText("DAY " + day.ToString());
    }

    public void Progress(int index) {
    
        SetCharacterAlpha(currentIndex, 1);

        SlideTextLeft();

        currentIndex = index;
    }

    public void SlideTextLeft()
    {
        float slideAmount = GetCharacterSlideWidth(currentIndex);

        SetWakeUpTextLerp(wakeUpText.rectTransform.localPosition,
                          lerpTargetPosition + (Vector3.left * slideAmount));
    }

    public void ResetWakeUpText()
    {
        SetCharacterAlpha(currentIndex, 1);

        currentIndex = 0;

        wakeUpTextInitialLocalDayPosition = wakeUpTextPositionStart + (Vector3.left * (GetWidthOfCharacter(0) / 2));

        SetWakeUpTextLerp(wakeUpText.rectTransform.position,
                          wakeUpTextInitialLocalDayPosition);
    }

    public void EnableWakeUpText(string wakeUpTextString)
    {
        wakeUpText.SetText(wakeUpTextString);
        ResetWakeUpText();
        wakeUpText.enabled = true;
    }

    public void DisableWakeUpText()
    {
        wakeUpText.enabled = false;
    }

    private void SetCharacterAlpha(int characterIndex, float alpha) {

        wakeUpText.ForceMeshUpdate();

        TMP_CharacterInfo info = wakeUpText.textInfo.characterInfo[characterIndex];

        int materialIndex = info.materialReferenceIndex;
        int vertexIndex = info.vertexIndex;
   

        float alphaValue = ((int) (255 * alpha));

        Color32[] vertexColors = wakeUpText.textInfo.meshInfo[materialIndex].colors32;

        vertexColors[vertexIndex + 0].a = (byte) alphaValue;
        vertexColors[vertexIndex + 1].a = (byte) alphaValue;
        vertexColors[vertexIndex + 2].a = (byte) alphaValue;
        vertexColors[vertexIndex + 3].a = (byte) alphaValue;

        wakeUpText.UpdateVertexData();
    }

    private void SetWakeUpTextLerp(Vector3 startPositon, Vector3 targetPosition) {
        lerpStartPosition = startPositon;
        lerpTargetPosition = targetPosition;
        remainingWakeUpTextSlideTime = wakeUpTextSlideTime;
    }

    private float GetWidthOfCharacter(int characterIndex) {

        wakeUpText.ForceMeshUpdate(true);

        TMP_CharacterInfo info = wakeUpText.textInfo.characterInfo[characterIndex];

        Debug.Log(info.character);

        return (info.vertex_BR.position.x - info.vertex_BL.position.x);

    }

    private float GetCharacterSlideWidth(int characterIndex) {

        wakeUpText.ForceMeshUpdate(true);

        TMP_CharacterInfo infoFirstCharacter = wakeUpText.textInfo.characterInfo[characterIndex];
        TMP_CharacterInfo infoSecondCharacter = wakeUpText.textInfo.characterInfo[characterIndex + 1];

        float firstCharacterMidpoint = infoFirstCharacter.vertex_BL.position.x + (Mathf.Abs(infoFirstCharacter.vertex_BL.position.x - infoFirstCharacter.vertex_BR.position.x) / 2);
        float secondCharacterMidpoint = infoSecondCharacter.vertex_BL.position.x + (Mathf.Abs(infoSecondCharacter.vertex_BL.position.x - infoSecondCharacter.vertex_BR.position.x) / 2);

        return Mathf.Abs(firstCharacterMidpoint - secondCharacterMidpoint);

    }
}
