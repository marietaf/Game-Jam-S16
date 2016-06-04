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
    private Vector3 velocity;

    void Awake()
    {
        //_animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController2D>();

        // listen to some events for illustration purposes
        //characterController.onControllerCollidedEvent += onControllerCollider;
        //characterController.onTriggerEnterEvent += onTriggerEnterEvent;
        //characterController.onTriggerExitEvent += onTriggerExitEvent;
    }

    void Start () {
	
	}
	
	public void Move(float hoizontalMovement, bool jump, bool dropDown) {
        if (characterController.isGrounded)
        {
            velocity.y = 0;
        }
        normalizedHorizontalSpeed = hoizontalMovement;
        if (transform.localScale.x > 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (characterController.isGrounded)
        {
            //_animator.Play( Animator.StringToHash( "Run" ) );
        }
        if (hoizontalMovement == 0)
        {
            if (characterController.isGrounded)
            {
                //_animator.Play( Animator.StringToHash( "Idle" ) );
            }
        }

        // we can only jump whilst grounded
        if (characterController.isGrounded && jump)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            //_animator.Play( Animator.StringToHash( "Jump" ) );
        }


        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = characterController.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        velocity.x = Mathf.Lerp(velocity.x, hoizontalMovement * runSpeed, Time.deltaTime * smoothedMovementFactor);

        // apply gravity before moving
        velocity.y += gravity * Time.deltaTime;

        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets uf jump down through one way platforms
        if (characterController.isGrounded && dropDown)
        {
            velocity.y *= 3f;
            characterController.ignoreOneWayPlatformsThisFrame = true;
        }

        characterController.move(velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        velocity = characterController.velocity;
    }
}
