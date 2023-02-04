using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpTextUIManager : MonoBehaviour
{
    public GameObject wakeUpTextPrefab;

    public Transform wakeUpTextTransform;

    private WakeUpTextUIController currentWakeUpTextUIController;

    public void InitializeWakeUpText(string wakeUpText) {

        GameObject dialoguePrefabInstance = Instantiate(wakeUpTextPrefab, transform.position, Quaternion.identity);
        dialoguePrefabInstance.transform.SetParent(wakeUpTextTransform, false);

        currentWakeUpTextUIController = dialoguePrefabInstance.GetComponent<WakeUpTextUIController>();

        currentWakeUpTextUIController.Init(wakeUpText);
    }

    public bool HasActiveWakeUpText() {
        return currentWakeUpTextUIController != null;
    }
}
