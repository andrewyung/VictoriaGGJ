using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyInitialForce : MonoBehaviour {

    public Vector2 force;

	// Use this for initialization
	void Start () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(force);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
