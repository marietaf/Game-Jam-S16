using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        ToggleGravityDirection(otherObject);
    }

    void OnTriggerExit2D(Collider2D otherObject)
    {
        ToggleGravityDirection(otherObject);
    }

    void ToggleGravityDirection(Collider2D otherObject)
    {
        if (otherObject.gameObject.layer == 8)//layer 8 is player
        {
            PlayerMovement movementComponent = otherObject.GetComponent<PlayerMovement>();
            movementComponent.gravity *= -1;
        }
    }
}
