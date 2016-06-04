using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerMovement : MonoBehaviour {
    
    // movement config
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    private CharacterController2D characterController;
    //private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;

    void Awake()
    {
        //_animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController2D>();

        // listen to some events for illustration purposes
        //characterController.onControllerCollidedEvent += onControllerCollider;
        //characterController.onTriggerEnterEvent += onTriggerEnterEvent;
        //characterController.onTriggerExitEvent += onTriggerExitEvent;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
