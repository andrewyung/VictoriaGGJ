using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

    public GameObject normalBackgroundGO;
    public GameObject hellBackgroundGO;

    public float alphaChangeRate = 1;

    enum BackgroundState { normal, hell };
    private BackgroundState currentState;

    private SpriteRenderer normalSR;
    private SpriteRenderer hellSR;

	// Use this for initialization
	void Start () {
        normalSR = normalBackgroundGO.GetComponent<SpriteRenderer>();
        hellSR = hellBackgroundGO.GetComponent<SpriteRenderer>();

        currentState = BackgroundState.normal;

        StartCoroutine(switchToBackground(BackgroundState.hell));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator switchToBackground(BackgroundState bgState)
    {
        Debug.Log("at");
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
}
