using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUIManager : Singleton<DialogueUIManager>
{
    public GameObject dialoguePrefab;

    public Transform dialoguePanelTransform;

    private DialogueUIController currentDialogueUIController;

    public void CreateDialogue(Dialogue[] dialogueList) {

        GameObject dialoguePrefabInstance = Instantiate(dialoguePrefab, transform.position, Quaternion.identity);
        dialoguePrefabInstance.transform.SetParent(dialoguePanelTransform, false);

        currentDialogueUIController = dialoguePrefabInstance.GetComponent<DialogueUIController>();

        currentDialogueUIController.StartDialogue(dialogueList);
    }

    public bool HasActiveDialogue() {
        return currentDialogueUIController != null;
    }
}
