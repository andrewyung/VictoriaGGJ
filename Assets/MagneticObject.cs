using UnityEngine;
using System.Collections;

public class MagneticObjectt : MonoBehaviour {

    [SerializeField]
    private float forceAmount;

    [SerializeField]
    private float forceApplyFrequency;

    private bool isApplyingForce = false;

    private Rigidbody2D playerRigidbody;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        playerRigidbody = collider.GetComponent<Rigidbody2D>();

        if (!isApplyingForce)
        {
            StartCoroutine(applyForce(collider));
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //remove player from being applied force
        playerRigidbody = null;
    }

    IEnumerator applyForce(Collider2D collider)
    {
        isApplyingForce = true;
        while (playerRigidbody != null)
        {
            playerRigidbody.AddForce(-(collider.transform.position - transform.position) * forceAmount);
            Debug.Log(-(collider.transform.position - transform.position));
            yield return new WaitForSeconds(forceApplyFrequency);
        }
        isApplyingForce = false;
    }
}
