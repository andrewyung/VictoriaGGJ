using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;
    public RoomScrolling roomScrolling;

    public float playerXStartOffsetValue = -4.2f;

    private float playerXThreshold;
    private float cameraPosition;

	// Use this for initialization
	void Start () {
        playerXThreshold = playerXStartOffsetValue;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.transform.position.x > playerXThreshold)
        {
            transform.position = new Vector3(transform.position.x + (player.transform.position.x - playerXThreshold), transform.position.y, transform.position.z);

            roomScrolling.scroll(player.transform.position.x - playerXThreshold);

            playerXThreshold = player.transform.position.x;
        }
	}
}
