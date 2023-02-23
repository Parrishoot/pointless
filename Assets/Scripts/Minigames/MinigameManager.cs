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

    public IMinigameController currentController;

    public Transform leftGameAnchor;
    public Transform rightGameAnchor;

    private Dictionary<MINIGAME, GameObject> minigameControllerPrefabs = new Dictionary<MINIGAME, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        minigameControllerPrefabs.Add(MINIGAME.VIDEO_GAME, videoGameControllerPrefab);
        minigameControllerPrefabs.Add(MINIGAME.DISHES, dishesControllerPrefab);
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

    void Update() {
        if(InProgress() && currentController.IsFinished()) {
            EndMinigame();
        }
    }

    public void BeginMinigame(MINIGAME minigame, CameraController.POSITION cameraPosition)
    {
        PlayerMovementManager.GetInstance().DisableMovement();

        GameObject currentControllerObject = Instantiate(minigameControllerPrefabs[minigame]);

        Transform parentTransform = cameraPosition == CameraController.POSITION.LEFT ? leftGameAnchor : rightGameAnchor;
        currentControllerObject.transform.SetParent(parentTransform, false);

        currentController = currentControllerObject.GetComponent<IMinigameController>();

        CameraController.GetInstance().SetCameraPosition(cameraPosition);
    }

    public void EndMinigame()
    {
        // TODO: ACTUALLY DO THIS
        currentController.Teardown();
        currentController = null;
        CameraController.GetInstance().SetCameraPosition(CameraController.POSITION.CENTER);
        PlayerMovementManager.GetInstance().EnableMovement();
    }

    public bool InProgress()
    {
        return currentController != null;
    }
}
