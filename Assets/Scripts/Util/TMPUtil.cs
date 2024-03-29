using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TMPUtil
{
    public static TMP_CharacterInfo GetCharcterInfo(TMP_Text text, int characterIndex) {
        return  text.textInfo.characterInfo[characterIndex];
    }

    public static Color32[] GetCharacterMaterialColorArray(TextMeshProUGUI text, int characterIndex) {
        TMP_CharacterInfo info = TMPUtil.GetCharcterInfo(text, characterIndex);
        int materialIndex = info.materialReferenceIndex;
        return text.textInfo.meshInfo[materialIndex].colors32;
    }

    public static void SetCharacterAlpha(TextMeshProUGUI text, int characterIndex, float alpha, bool updateVertexData = true, bool forceMeshUpdate = true) {

        TMP_CharacterInfo info = GetCharcterInfo(text, characterIndex);
        if(!info.isVisible) {
            return;
        }

        if(forceMeshUpdate) {
            text.ForceMeshUpdate();
        }

        Color32[] vertexColors = TMPUtil.GetCharacterMaterialColorArray(text, characterIndex);

        float alphaValue = ((int) (255 * alpha));

        vertexColors[info.vertexIndex + 0].a = (byte) alphaValue;
        vertexColors[info.vertexIndex + 1].a = (byte) alphaValue;
        vertexColors[info.vertexIndex + 2].a = (byte) alphaValue;
        vertexColors[info.vertexIndex + 3].a = (byte) alphaValue;

        if(updateVertexData) {
            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }
    }

    public static Vector2 GetCharacterCenter(TMP_Text text, int characterIndex) {

        TMP_CharacterInfo info = GetCharcterInfo(text, characterIndex);
        int vertexIndex = info.vertexIndex;

        float xPos = (info.vertex_BL.position.x + info.vertex_BR.position.x) / 2;
        float yPos = (info.vertex_TL.position.y + info.vertex_BL.position.y) / 2;

        return new Vector3(xPos, yPos);

    }

    public static void SetCharacterPosition(TextMeshProUGUI text, int characterIndex, Vector2 pos, bool updateVertexData = true, bool forceMeshUpdate = true) {

        
        TMP_CharacterInfo info = GetCharcterInfo(text, characterIndex);
        if(!info.isVisible) {
            return;
        }

        if(forceMeshUpdate) {
            text.ForceMeshUpdate();
        }

       
    }

    public static float GetWidthOfCharacter(TextMeshProUGUI text, int characterIndex) {

        text.ForceMeshUpdate(true);

        TMP_CharacterInfo info = GetCharcterInfo(text, characterIndex);

        return Mathf.Abs(info.vertex_BR.position.x - info.vertex_BL.position.x);

    }
}
