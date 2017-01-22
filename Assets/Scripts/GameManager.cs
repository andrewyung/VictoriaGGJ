using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool startedGame = true;
    private bool endedGame = true;

    private static GameManager gameManager;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void beginningShift()
    {
        BackgroundManager.switchBackground(BackgroundManager.BackgroundState.hell);
        RoomScrolling.isScrolling = true;
    }

    void endingShift()
    {
        BackgroundManager.switchBackground(BackgroundManager.BackgroundState.normal);
        RoomScrolling.isScrolling = false;
    }
}
