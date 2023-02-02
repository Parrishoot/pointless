using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUIController : MonoBehaviour
{
    public TMP_Text dialogueText;

    public AnimationCurve letterCurve;

    public float letterOffsetScale = 5f;

    public float letterFadeRatio = 3f;

    public float characterAppearSpeed = .1f;

    public float dialogueSpawnTime = .1f;

    private Dialogue currentDialogue;

    private float currentDialogueSpawnTime = 0f;

    private float currentAppearLerpTime = 0f;

    private int currentIndex = 0;

    private List<CharacterSpawn> characterSpawns = new List<CharacterSpawn>();

    private bool finished = true;

    private class CharacterSpawn {

        private float remainingLerpTime;

        private int characterIndex;

        private Vector2 position;

        private bool processed = false;

        public CharacterSpawn(int characterIndex, float remainingLerpTime, Vector2 position)
        {
            this.remainingLerpTime = remainingLerpTime;
            this.characterIndex = characterIndex;
            this.Position = position;
        }

        public float RemainingLerpTime { get => remainingLerpTime; set => remainingLerpTime = value; }
        public int CharacterIndex { get => characterIndex; set => characterIndex = value; }
        public Vector2 Position { get => position; set => position = value; }
        public bool Processed { get => processed; set => processed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = dialogueText.text.Length;
    }   

    // // Update is called once per frame
    void Update()
    {
        dialogueText.ForceMeshUpdate();

        if(!spawnMore()) {

            if(currentDialogueSpawnTime > 0) {
                currentDialogueSpawnTime -= Time.deltaTime;
            }
            else {
                dialogueText.maxVisibleCharacters++;

                dialogueText.ForceMeshUpdate();
                characterSpawns.Add(new CharacterSpawn(currentIndex, characterAppearSpeed, TMPUtil.GetCharacterCenter(dialogueText, currentIndex)));

                currentIndex++;
                currentDialogueSpawnTime = dialogueSpawnTime;
            }
        }

        List<CharacterSpawn> removeCharacterSpawns = new List<CharacterSpawn>();

        Mesh mesh = dialogueText.mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = mesh.colors;

        bool allCharactersProcessed = true;

        // Go through each of the spawned characters
        foreach(CharacterSpawn characterSpawn in characterSpawns) {

            allCharactersProcessed = allCharactersProcessed && characterSpawn.Processed;            

            // Removed spaces and other blank characters
            if(dialogueText.textInfo.characterInfo[characterSpawn.CharacterIndex].character == ' ') {
                removeCharacterSpawns.Add(characterSpawn);
                continue;
            }

            // Get the animation lerp percentage
            float lerpPercentage = (characterAppearSpeed - characterSpawn.RemainingLerpTime) / characterAppearSpeed;

            // Find the offset position
            Vector2 targetPosition = Vector2.Lerp(characterSpawn.Position + new Vector2(0, letterCurve.Evaluate(lerpPercentage) * letterOffsetScale), characterSpawn.Position, lerpPercentage);
            Vector2 currentPosition = TMPUtil.GetCharacterCenter(dialogueText, characterSpawn.CharacterIndex);

            Vector2 offset = targetPosition - currentPosition;

            // Find the fade in alpha
            float alpha = Mathf.Clamp(Mathf.Lerp(0, letterFadeRatio, lerpPercentage), 0, 1);

            int vertexIndex = dialogueText.textInfo.characterInfo[characterSpawn.CharacterIndex].vertexIndex;

            // If a character is finsihed animating, keep it's alpha at 1
            /*
            vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
            THIS IS WHERE I THINK THE WASTE IS
            vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
            */
            if(characterSpawn.RemainingLerpTime == 0) {
                removeCharacterSpawns.Add(characterSpawn);
            }
            // Otherwise, animate it moving and update the alpha
            else {
                vertices[vertexIndex].y += offset.y;
                vertices[vertexIndex + 1].y += offset.y;
                vertices[vertexIndex + 2].y += offset.y;
                vertices[vertexIndex + 3].y += offset.y;
            }

            // colors[vertexIndex].a = 1;
            // colors[vertexIndex + 1].a = 1;
            // colors[vertexIndex + 2].a = 1;
            // colors[vertexIndex + 3].a = 1; 
        
            // Update the Lerp time
            characterSpawn.RemainingLerpTime = Mathf.Max(characterSpawn.RemainingLerpTime - Time.deltaTime, 0);
        }


        mesh.vertices = vertices;
        dialogueText.canvasRenderer.SetMesh(mesh);

        foreach(CharacterSpawn removedCharacterSpawn in removeCharacterSpawns) {
            characterSpawns.Remove(removedCharacterSpawn);
        }

        if(allCharactersProcessed) {
            finished = true;
        }

        if(Input.GetKeyDown(KeyCode.G)) {
            currentIndex = 0;
            dialogueText.maxVisibleCharacters = 0;
            finished = false;
            characterSpawns.Clear();
        }
        

    }

    // public void SetDialogue(Dialogue dialogue) {
    //     this.currentDialogue = dialogue;

    //     dialogueText.SetText(currentDialogue.Text);
    //     dialogueText.ForceMeshUpdate();

    //     ResetMeshAlpha();

    //     currentIndex = 0;
    // }

    private void ResetMeshAlpha() {


    }

    public bool spawnMore() { 
        return (currentIndex == dialogueText.text.Length);
    }

    public bool isFinished() {
        return finished;
    }
}
