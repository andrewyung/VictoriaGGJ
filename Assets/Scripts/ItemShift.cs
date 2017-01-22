using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShift : MonoBehaviour {

    public float speed = 1;

    public Vector2 offset;

    public void doShift()
    {
        StartCoroutine(startShift());
    }

    //only supports shifting to the right
    IEnumerator startShift()
    {
        while (transform.position.x < offset.x)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
