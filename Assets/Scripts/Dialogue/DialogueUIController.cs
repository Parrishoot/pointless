using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUIController : MonoBehaviour
{

    private class CharacterSpawn {

        private float remainingLerpTime;

        private int characterIndex;

        private Vector2 position;

        public CharacterSpawn(int characterIndex, float remainingLerpTime, Vector2 position)
        {
            this.remainingLerpTime = remainingLerpTime;
            this.characterIndex = characterIndex;
            this.Position = position;
        }

        public float RemainingLerpTime { get => remainingLerpTime; set => remainingLerpTime = value; }
        public int CharacterIndex { get => characterIndex; set => characterIndex = value; }
        public Vector2 Position { get => position; set => position = value; }
    }

    public float characterAppearSpeed = 1f;

    public Vector2 characterOffset = new Vector2(0, 2);

    public TextMeshProUGUI dialogueText;

    private Dialogue currentDialogue;

    private float currentDialogueSpawnTime = 0f;

    private float currentAppearLerpTime = 0f;

    private int currentIndex = 0;

    private List<CharacterSpawn> characterSpawns = new List<CharacterSpawn>();

    // Update is called once per frame
    void Update()
    {
        if(!spawnMore()) {

            if(currentDialogueSpawnTime > 0) {
                currentDialogueSpawnTime -= Time.deltaTime;
            }
            else {
                characterSpawns.Add(new CharacterSpawn(currentIndex, characterAppearSpeed, TMPUtil.GetCharacterCenter(dialogueText, currentIndex)));
                currentDialogueSpawnTime = currentDialogue.Speed;
                currentIndex++;
            }
        }

        List<CharacterSpawn> removeCharacterSpawns = new List<CharacterSpawn>();

        foreach(CharacterSpawn characterSpawn in characterSpawns) {

            float lerpPercentage = (characterAppearSpeed - characterSpawn.RemainingLerpTime) / characterAppearSpeed;

            characterSpawn.RemainingLerpTime -= Time.deltaTime;

            if(characterSpawn.RemainingLerpTime <= 0){
                TMPUtil.SetCharacterAlpha(dialogueText, characterSpawn.CharacterIndex, 1, true, false);
                TMPUtil.SetCharacterAlpha(dialogueText, characterSpawn.CharacterIndex, 1, true, false);
                removeCharacterSpawns.Add(characterSpawn);
                Debug.Log("Removing");
            }
            else {
                TMPUtil.SetCharacterAlpha(dialogueText, characterSpawn.CharacterIndex, Mathf.Lerp(0, 1, lerpPercentage), true, false);
            }
        }

        foreach(CharacterSpawn removedCharacterSpawn in removeCharacterSpawns) {
            characterSpawns.Remove(removedCharacterSpawn);
        }

        if(Input.GetKeyDown(KeyCode.G)) {
             // TODO: Replace this with the real thing
            SetDialogue(new Dialogue("This is some text to test the dialogue system."));
        }
    }

    // IEnumerator SpawnCharacter(int characterIndex) {
        
    //     float timeLeft = characterAppearSpeed;

    //     Vector2 finalPosition = TMPUtil.GetCharacterCenter(dialogueText, characterIndex);
    //     Vector2 initialPosition = finalPosition + characterOffset; 
        
    //     Debug.Log(initialPosition);
    //     Debug.Log(finalPosition);

    //     while(timeLeft > 0) {

    //         float timePercentage = (characterAppearSpeed - timeLeft) / characterAppearSpeed;

    //         // TMPUtil.SetCharacterPosition(dialogueText, characterIndex, Vector2.Lerp(initialPosition, finalPosition, timePercentage), false, false);
            

    //         timeLeft -= Time.deltaTime;

    //         yield return null;
    //     }

    //     TMPUtil.SetCharacterAlpha(dialogueText, characterIndex, 1, true, false);

    //     yield return null;
    // }

    public void SetDialogue(Dialogue dialogue) {
        this.currentDialogue = dialogue;

        dialogueText.SetText(currentDialogue.Text);
        dialogueText.ForceMeshUpdate();

        ResetMeshAlpha();

        currentIndex = 0;
    }

    private void ResetMeshAlpha() {

        for(int i = 0; i < dialogueText.text.Length; i++) {
            TMPUtil.SetCharacterAlpha(dialogueText, i, 0, true, false);            
        }
    }

    public bool spawnMore() { 
        return currentDialogue == null || (currentIndex == currentDialogue.Text.Length);
    }

    public bool isFinished() {
        return currentDialogue == null || (currentIndex == currentDialogue.Text.Length && characterSpawns.Count == 0);
    }
}
