using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject ground;

    public GameObject firstStageObjects;
    public GameObject secondStageObjects;

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

    public static void beginningShift()
    {
        BackgroundManager.switchBackground(BackgroundManager.BackgroundState.hell);
        gameManager.ground.transform.position = new Vector3(0, -2.46f, gameManager.ground.transform.position.z);
        gameManager.firstStageObjects.SetActive(false);
        gameManager.secondStageObjects.SetActive(true);
        //RoomScrolling.isScrolling = true;
    }

    public static void endingShift()
    {
        BackgroundManager.switchBackground(BackgroundManager.BackgroundState.normal);
        //RoomScrolling.isScrolling = false;
    }
}
