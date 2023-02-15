using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : Singleton<MinigameManager>
{

    public enum MINIGAME
    {
        VIDEO_GAME,
        DISHES
    }

    public GameObject videoGameControllerPrefab;
    public GameObject dishesControllerPrefab;

    public Transform leftGameAnchor;
    public Transform rightGameAnchor;

    private Dictionary<MINIGAME, GameObject> minigameControllerPrefabs = new Dictionary<MINIGAME, GameObject>();
    private MinigameController currentController;

    // This is sloppy but it works!
    bool initializing = false;

    // Start is called before the first frame update
    void Start()
    {
        minigameControllerPrefabs.Add(MINIGAME.VIDEO_GAME, videoGameControllerPrefab);
        minigameControllerPrefabs.Add(MINIGAME.DISHES, dishesControllerPrefab);
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
        PlayerMovementManager.GetInstance().DisableMovement();

        GameObject minigameControllerPrefab = Instantiate(minigameControllerPrefabs[minigame]);

        Transform parentTransform = cameraPosition == CameraController.POSITION.LEFT ? leftGameAnchor : rightGameAnchor;
        minigameControllerPrefab.transform.SetParent(parentTransform, false);
        
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
        PlayerMovementManager.GetInstance().EnableMovement();
    }

    public bool InProgress()
    {
        return currentController != null;
    }
}
