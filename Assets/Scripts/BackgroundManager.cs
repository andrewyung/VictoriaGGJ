using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

    public GameObject normalBackgroundGO;
    public GameObject hellBackgroundGO;

    public float alphaChangeRate = 1;

    public enum BackgroundState { normal, hell };
    private BackgroundState currentState;

    private SpriteRenderer normalSR;
    private SpriteRenderer hellSR;

    private static BackgroundManager backgroundManager;

    void Awake()
    {
        if (backgroundManager == null)
        {
            backgroundManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

	// Use this for initialization
	void Start () {
        normalSR = normalBackgroundGO.GetComponent<SpriteRenderer>();
        hellSR = hellBackgroundGO.GetComponent<SpriteRenderer>();

        currentState = BackgroundState.normal;
    }
	
	public static void switchBackground(BackgroundState bgState)
    {
        backgroundManager.StartCoroutine(backgroundManager.switchToBackground(bgState));
    }

    IEnumerator switchToBackground(BackgroundState bgState)
    {
        if (currentState != bgState)
        {
            switch(bgState)
            {
                case BackgroundState.normal:
                    while (hellSR.color.a > 0)
                    {
                        //decrease hell alpha
                        Color color = hellSR.color;
                        color.a -= alphaChangeRate * Time.deltaTime;
                        hellSR.color = color;

                        //increase normal alpha
                        color.a = 1 - color.a;//invert alpha for normal
                        normalSR.color = color;

                        yield return new WaitForEndOfFrame();
                    }
                    break;
                case BackgroundState.hell:
                    while (normalSR.color.a > 0)
                    {
                        //decrease normal alpha
                        Color color = normalSR.color;
                        color.a -= alphaChangeRate * Time.deltaTime;
                        normalSR.color = color;

                        //increase hell alpha
                        color.a = 1 - color.a;//invert alpha for normal
                        hellSR.color = color;

                        yield return new WaitForEndOfFrame();
                    }
                    break;
            }
        }
    }

    IEnumerator propSpawning()
    {
        while (currentState == BackgroundState.hell)
        {

            yield return new WaitForEndOfFrame();
        }
    }
}
