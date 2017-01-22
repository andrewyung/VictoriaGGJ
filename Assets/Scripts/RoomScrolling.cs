using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScrolling : MonoBehaviour {
    
    public float roomLength;

    public static bool isScrolling = true;

    private Vector2 originPosition;

    private float offset;
    private int scrollMultiplier;

    void Start()
    {
        originPosition = transform.position;
    }

    public void scroll(float amount)
    {
        offset += amount;
        transform.position = new Vector2(originPosition.x + (scrollMultiplier * roomLength), transform.position.y);

        if (offset > roomLength)
        {
            scrollMultiplier++;
            offset -= roomLength;
        }
    }
}
