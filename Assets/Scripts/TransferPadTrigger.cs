using UnityEngine;
using System.Collections;
using Prime31;

public class TransferPadTrigger : MonoBehaviour {

    private AreaEffector2D transferPadEffector;

    // Use this for initialization
    void Awake () {
        transferPadEffector = GetComponent<AreaEffector2D>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter2D (Collider2D other) {
        Debug.Log("enter");
        PlayerMovement colliderMovement = other.GetComponent<PlayerMovement>();
        float movementAngleRads = transferPadEffector.forceAngle* Mathf.Deg2Rad;
        float force = transferPadEffector.forceMagnitude;
        colliderMovement.TriggeredMove(force, movementAngleRads);
        Debug.Log("added up");
    }

}
