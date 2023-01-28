using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : Singleton<MinigameManager>
{

    public enum MINIGAME
    {
        VIDEO_GAME
    }

    public GameObject videoGameControllerPrefab;

    private Dictionary<MINIGAME, GameObject> minigameControllerPrefabs = new Dictionary<MINIGAME, GameObject>();
    private MinigameController currentController;

    // This is sloppy but it works!
    bool initializing = false;

    // Start is called before the first frame update
    void Start()
    {
        minigameControllerPrefabs.Add(MINIGAME.VIDEO_GAME, videoGameControllerPrefab);
    }

    public void Update()
    {
        initializing = false;
    }

    public void SwapMinigameState(MINIGAME minigame, CameraController.POSITION cameraPosition)
    {
        if (!InProgress())
        {
            BeginMinigame(minigame, cameraPosition);
        }
        else
        {
            EndMinigame();
        }
        
    }

    public void BeginMinigame(MINIGAME minigame, CameraController.POSITION cameraPosition)
    {
        PlayerMovementController.GetInstance().Disable();

        GameObject minigameControllerPrefab = Instantiate(minigameControllerPrefabs[minigame]);
        currentController = minigameControllerPrefab.GetComponent<MinigameController>();
        initializing = true;

        CameraController.GetInstance().SetCameraPosition(cameraPosition);
    }

    public void EndMinigame()
    {
        // TODO: ACTUALLY DO THIS
        Destroy(currentController.gameObject);
        currentController = null;
        CameraController.GetInstance().SetCameraPosition(CameraController.POSITION.CENTER);
        PlayerMovementController.GetInstance().Enable();
    }

    public bool InProgress()
    {
        return currentController != null;
    }
}
