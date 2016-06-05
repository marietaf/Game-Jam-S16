using UnityEngine;
using System.Collections;
using Prime31;

public class TransferPad: MonoBehaviour {

    /// <summary>
    /// Velocity/momentum transfer system requiring 2 players/characters.
    /// </summary>
    private Collider2D col1, col2;
    private PlayerMovement enterMovement, exitMovement;
    private Vector3 transferVelocity;


    // Initialize the 2 expected colliders interacting with the transfer pad trigger
    void Awake () {
        col1 = null;
        col2 = null;
        transferVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update () {
	}

    void OnTriggerEnter2D (Collider2D entering) {

        if (col1 == null)
        {
            col1 = entering;
            return;
        } else if (col1 != entering) {
            col2 = entering; }
        //Debug.Log("a thing entered");

        enterMovement = entering.GetComponent<PlayerMovement>();
        
        //has loss. whyyyyyyyy
        transferVelocity = enterMovement.GetVelocity();
        enterMovement.TriggeredMove(-1*transferVelocity);

        exitMovement = col1.GetComponent<PlayerMovement>();
        exitMovement.TriggeredMove(transferVelocity);
        //Debug.Log("a thing happened");
    }

    
    void OnTriggerExit2D(Collider2D exiting)
    {
        if (col1 == exiting)
        {
            col1 = col2;
            col2 = null;
        }
        else if (col2 == exiting) col2 = null;
        //Debug.Log("a thing exits" + exiting.ToString());
    }

}
