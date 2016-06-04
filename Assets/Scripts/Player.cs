using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    PlayerMovement movementComponent;
    InputBaseClass inputComponent;
    PlayerClone cloneComponent;

	// Use this for initialization
	void Start () {
        movementComponent = GetComponent<PlayerMovement>();
        inputComponent = GetComponent<InputBaseClass>();
        cloneComponent = GetComponent<PlayerClone>();
	}
	
	// Update is called once per frame
	void Update () {
        movementComponent.Move(inputComponent.GetHorizontalMovement(), inputComponent.GetJump(), inputComponent.GetDropDown());
        if(cloneComponent != null)
            cloneComponent.ToggleClone(inputComponent.GetToggleClone());
    }
}
