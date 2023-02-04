using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WakeUpTextUIController : MonoBehaviour
{
    
    public TextMeshProUGUI wakeUpText;

    public float flashSpeed = 1f;

    public float wakeUpTextSlideTime = .2f;

    private float remainingWakeUpTextSlideTime = 0f;

    private Vector3 wakeUpTextPositionStart;

    private Vector3 wakeUpTextInitialLocalDayPosition;

    private Vector3 lerpTargetPosition;

    private Vector3 lerpStartPosition;

    private int currentIndex = 0;

    public void Update() {

        if(remainingWakeUpTextSlideTime >= 0) {
            remainingWakeUpTextSlideTime -= Time.deltaTime;
            float time = Mathf.Clamp((wakeUpTextSlideTime - remainingWakeUpTextSlideTime) / wakeUpTextSlideTime, 0f, 1f);
            wakeUpText.rectTransform.localPosition = Vector3.Lerp(lerpStartPosition, lerpTargetPosition, Mathf.SmoothStep(0f, 1f, time));
        }
        else if(lerpTargetPosition != wakeUpText.rectTransform.localPosition) {
            wakeUpText.rectTransform.localPosition = lerpTargetPosition;
        }

        TMPUtil.SetCharacterAlpha(wakeUpText, currentIndex, Mathf.PingPong(Time.time * flashSpeed, 1), true, false);

        if(InputManager.GetInstance().SpecificLetterKeyPressedThisFrame(wakeUpText.text[currentIndex].ToString())) {
            Progress();
        }
        else if(InputManager.GetInstance().AnyKeyPressedThisFrame())
        {
            ResetWakeUpText();
        }
    }

    public void Progress() {

        if(currentIndex == wakeUpText.text.Length - 1) {
            Destroy(gameObject);
        }
        else {
            TMPUtil.SetCharacterAlpha(wakeUpText, currentIndex, 1, true, false);
            SlideTextLeft();
            currentIndex++;
        }
    }

    public void SlideTextLeft()
    {
        float slideAmount = GetCharacterSlideWidth(currentIndex);

        SetWakeUpTextLerp(wakeUpText.rectTransform.localPosition,
                          lerpTargetPosition + (Vector3.left * slideAmount));
    }

    public void ResetWakeUpText()
    {
        TMPUtil.SetCharacterAlpha(wakeUpText, currentIndex, 1);

        currentIndex = 0;

        wakeUpTextInitialLocalDayPosition = wakeUpTextPositionStart + (Vector3.left * (TMPUtil.GetWidthOfCharacter(wakeUpText, 0) / 2));

        SetWakeUpTextLerp(wakeUpText.rectTransform.localPosition,
                          wakeUpTextInitialLocalDayPosition);
    }

    public void Init(string newWakeUpText) {
        wakeUpText.SetText(newWakeUpText);
        wakeUpTextPositionStart = wakeUpText.rectTransform.localPosition;
        ResetWakeUpText();
    }

    private void SetWakeUpTextLerp(Vector3 startPositon, Vector3 targetPosition) {
        lerpStartPosition = startPositon;
        lerpTargetPosition = targetPosition;
        remainingWakeUpTextSlideTime = wakeUpTextSlideTime;
    }

    private float GetCharacterSlideWidth(int characterIndex) {

        wakeUpText.ForceMeshUpdate(true);

        TMP_CharacterInfo infoFirstCharacter = TMPUtil.GetCharcterInfo(wakeUpText, characterIndex);
        TMP_CharacterInfo infoSecondCharacter = TMPUtil.GetCharcterInfo(wakeUpText, characterIndex + 1);;

        float firstCharacterMidpoint = infoFirstCharacter.vertex_BL.position.x + (Mathf.Abs(infoFirstCharacter.vertex_BL.position.x - infoFirstCharacter.vertex_BR.position.x) / 2);
        float secondCharacterMidpoint = infoSecondCharacter.vertex_BL.position.x + (Mathf.Abs(infoSecondCharacter.vertex_BL.position.x - infoSecondCharacter.vertex_BR.position.x) / 2);

        return Mathf.Abs(firstCharacterMidpoint - secondCharacterMidpoint);

    }
}
