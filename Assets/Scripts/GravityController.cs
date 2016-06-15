using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

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
            MovementBaseClass movementComponent = otherObject.GetComponent<MovementBaseClass>();
            movementComponent.gravity *= -1;
        }
    }
}
