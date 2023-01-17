using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : Singleton<PauseMenuManager>
{

    public GameObject pauseMenuCanvas;

    public enum PAUSE_MENU_STATE
    {
        ENABLED,
        DISABLED
    }

    private PAUSE_MENU_STATE pauseMenuState = PAUSE_MENU_STATE.DISABLED;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.PAUSE))
        {
            switch (pauseMenuState)
            {
                case PAUSE_MENU_STATE.DISABLED:
                    Time.timeScale = 0f;
                    pauseMenuCanvas.SetActive(true);
                    pauseMenuState = PAUSE_MENU_STATE.ENABLED;
                    break;

                case PAUSE_MENU_STATE.ENABLED:
                    Time.timeScale = 1f;
                    pauseMenuCanvas.SetActive(false);
                    pauseMenuState = PAUSE_MENU_STATE.DISABLED;
                    break;
            }
        }
    }

    public bool IsPaused()
    {
        return pauseMenuState == PAUSE_MENU_STATE.ENABLED;
    }
}