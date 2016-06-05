using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    MovementBaseClass movementComponent;
    InputBaseClass inputComponent;
    PlayerClone cloneComponent;

	void Start () {
        movementComponent = GetComponent<MovementBaseClass>();
        inputComponent = GetComponent<InputBaseClass>();
        cloneComponent = GetComponent<PlayerClone>();
	}
	
	void Update () {
        movementComponent.Move(inputComponent.GetHorizontalMovement(), inputComponent.GetJump(), inputComponent.GetDropDown());
        if (cloneComponent != null)
        {
            cloneComponent.ToggleClone(inputComponent.GetToggleClone());
        }
    }
}
