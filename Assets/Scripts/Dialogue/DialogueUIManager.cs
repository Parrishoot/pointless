using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUIManager : Singleton<DialogueUIManager>
{
    public GameObject dialoguePrefab;

    public Transform dialoguePanelTransform;

    private DialogueUIController currentDialogueUIController;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) && currentDialogueUIController == null) {

            List<Dialogue> dialogues = new List<Dialogue>();

            dialogues.Add(new Dialogue("Hey this is some dialogue!"));
            dialogues.Add(new Dialogue("This is some other slower dialogue!", .1f));

            GameObject dialoguePrefabInstance = Instantiate(dialoguePrefab, transform.position, Quaternion.identity);
            dialoguePrefabInstance.transform.SetParent(dialoguePanelTransform, false);

            currentDialogueUIController = dialoguePrefabInstance.GetComponent<DialogueUIController>();

            currentDialogueUIController.StartDialogue(dialogues);


        }
    }
}
