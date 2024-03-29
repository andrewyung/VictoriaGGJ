﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScrolling : MonoBehaviour {
    
    public float scrollSpeed;
    public float roomLength;

    private Vector2 originPosition;

    void Start()
    {
        originPosition = transform.position;
        originPosition.x -= roomLength / 2;
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, roomLength);
        transform.position = new Vector2(originPosition.x + x, transform.position.y);
    }
}
