using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    PlayerMovement movementComponent;
    InputBaseClass inputComponent;

	// Use this for initialization
	void Start () {
        movementComponent = GetComponent<PlayerMovement>();
        inputComponent = GetComponent<InputBaseClass>();
	}
	
	// Update is called once per frame
	void Update () {
        movementComponent.Move(inputComponent.GetHorizontalMovement(), inputComponent.GetJump(), inputComponent.GetDropDown());
	}
}
