using UnityEngine;
using System.Collections;

public class MagneticObject : MonoBehaviour {

    [SerializeField]
    private float forceAmount;

    private Rigidbody playerRigidbody;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        // get normalized direction of player
        Vector3 playerDirectionNormalized = (collider.transform.position - transform.position);

        //collider.GetComponent<Rigidbody>().addFor
    }
}
