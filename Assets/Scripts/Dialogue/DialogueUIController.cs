using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUIController : MonoBehaviour
{
    public TMP_Text dialogueText;

    public AnimationCurve letterCurve;

    public float letterOffsetScale = 5f;

    public float characterAppearSpeed = .1f;

    public float defaultDialogueSpeed = 0.0125f;

    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();

    private float currentDialogueSpeed = 0.0125f;

    private float remainingDialogueSpawnTime = 0f;

    private List<CharacterSpawn> characterSpawns = new List<CharacterSpawn>();

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

    // // Update is called once per frame
    void Update()
    {
        dialogueText.ForceMeshUpdate();

        // If there is still text to spawn, lets see it!
        if(charactersStillInvisible()) {

            if(remainingDialogueSpawnTime > 0) {
                remainingDialogueSpawnTime -= Time.deltaTime;
            }
            else {

                // Update the mesh with the new characters
                dialogueText.maxVisibleCharacters++;
                dialogueText.ForceMeshUpdate();

                // Set a new character spawn object
                int characterIndex = dialogueText.maxVisibleCharacters - 1;
                characterSpawns.Add(new CharacterSpawn(characterIndex, characterAppearSpeed, TMPUtil.GetCharacterCenter(dialogueText, characterIndex)));

                remainingDialogueSpawnTime = currentDialogueSpeed;
            }
        }

        List<CharacterSpawn> removeCharacterSpawns = new List<CharacterSpawn>();

        Mesh mesh = dialogueText.mesh;
        Vector3[] vertices = mesh.vertices;

        // Go through each of the spawned characters
        foreach(CharacterSpawn characterSpawn in characterSpawns) {        

            // Remove the spaces
            if(dialogueText.textInfo.characterInfo[characterSpawn.CharacterIndex].character == ' ') {
                removeCharacterSpawns.Add(characterSpawn);
                continue;
            }

            // Get the animation lerp percentage
            float lerpPercentage = (characterAppearSpeed - characterSpawn.RemainingLerpTime) / characterAppearSpeed;

            // Find the offset position
            Vector2 targetPosition = Vector2.Lerp(characterSpawn.Position + new Vector2(0, letterCurve.Evaluate(lerpPercentage) * letterOffsetScale), 
                                                  characterSpawn.Position, 
                                                  lerpPercentage);
            Vector2 currentPosition = TMPUtil.GetCharacterCenter(dialogueText, characterSpawn.CharacterIndex);
            Vector2 offset = targetPosition - currentPosition;

            int vertexIndex = dialogueText.textInfo.characterInfo[characterSpawn.CharacterIndex].vertexIndex;

            // Otherwise, animate it moving and update the alpha
            if(characterSpawn.RemainingLerpTime == 0) {
                removeCharacterSpawns.Add(characterSpawn);
            }
            else {
                vertices[vertexIndex].y += offset.y;
                vertices[vertexIndex + 1].y += offset.y;
                vertices[vertexIndex + 2].y += offset.y;
                vertices[vertexIndex + 3].y += offset.y;
            }
        
            // Update the Lerp time
            characterSpawn.RemainingLerpTime = Mathf.Max(characterSpawn.RemainingLerpTime - Time.deltaTime, 0);
        }

        mesh.vertices = vertices;
        dialogueText.canvasRenderer.SetMesh(mesh);

        foreach(CharacterSpawn removedCharacterSpawn in removeCharacterSpawns) {
            characterSpawns.Remove(removedCharacterSpawn);
        }

        if(Input.GetKeyDown(KeyCode.Space) && canMoveToNextDialogue()) {
            StartNextDialogue();
        }
    }

    public void StartDialogue(Dialogue[] dialogueList) {

        PlayerMovementManager.GetInstance().DisableMovement();

        dialogueQueue = new Queue<Dialogue>(dialogueList);
        StartNextDialogue();
    }

    public void StartNextDialogue() {
        if (dialogueQueue.Count != 0) {
            Dialogue nextDialogue = dialogueQueue.Dequeue();
            dialogueText.text = nextDialogue.Text;
            currentDialogueSpeed = nextDialogue.Speed == Dialogue.DEFAULT_SPEED ? defaultDialogueSpeed : nextDialogue.Speed;
            dialogueText.maxVisibleCharacters = 0;
            characterSpawns.Clear();
        }
        else {
            PlayerMovementManager.GetInstance().EnableMovement();
            Destroy(gameObject);
        }
    }

    public bool charactersStillInvisible() { 
        return (dialogueText.maxVisibleCharacters < dialogueText.text.Length);
    }

    public bool canMoveToNextDialogue() {
        return !charactersStillInvisible() && characterSpawns.Count == 0;
    }
}
