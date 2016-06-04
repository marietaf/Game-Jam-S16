using UnityEngine;
using System.Collections;
using Prime31;

public class TransferPadTrigger : MonoBehaviour {

    private AreaEffector2D transferPadEffector;
    private Collider2D receiver;
    private PlayerMovement enterMovement, exitMovement;
    private Vector3 transferVelocity, stopVelocity;

    // Use this for initialization
    void Awake () {
        transferPadEffector = GetComponent<AreaEffector2D>();
        receiver = null;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter2D (Collider2D entering) {
        // First to enter will receive momentum from next player
        if (receiver == null)
        {
            Debug.Log("enter first");
            receiver = entering;
            return;
        }

        Debug.Log("enter second");
        enterMovement = entering.GetComponent<PlayerMovement>();
        transferVelocity = enterMovement.GetVelocity();
        enterMovement.TriggeredMove(-1 * transferVelocity);

        exitMovement = receiver.GetComponent<PlayerMovement>();
        exitMovement.TriggeredMove(transferVelocity);

        receiver = entering;
        Debug.Log("added up");
    }

    void OnTriggerExit2D (Collider2D exiting)
    {
        if (receiver == exiting)
            receiver = null;
    }

}
