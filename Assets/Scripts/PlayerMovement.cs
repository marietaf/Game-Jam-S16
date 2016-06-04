using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerMovement : MonoBehaviour {

    // movement config
    //public bool isGravityDown = true;
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;
    
    private CharacterController2D characterController;
    //private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 velocity;
    private Vector3 triggeredVelocity;


    void Awake()
    {
        //_animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController2D>();

        // listen to some events for illustration purposes
        characterController.onControllerCollidedEvent += onControllerCollider;
        //characterController.onTriggerEnterEvent += onTriggerEnterEvent;
        //characterController.onTriggerExitEvent += onTriggerExitEvent;
    }

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f && gravity < 0)
            return;
        if (hit.normal.y == -1f && gravity > 0)
            return;


        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + characterController.collisionState + ", hit.normal: " + hit.normal );
    }

    void Start () {
	
	}
	
	public void Move(float horizontalMovement, bool jump, bool dropDown) {
        if (characterController.isGrounded)
        {
            velocity.y = 0;
        }

        if (transform.localScale.x > 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (characterController.isGrounded)
        {
            //_animator.Play( Animator.StringToHash( "Run" ) );
        }
        if (horizontalMovement == 0)
        {
            if (characterController.isGrounded)
            {
                //_animator.Play( Animator.StringToHash( "Idle" ) );
            }
        }

        // we can only jump whilst grounded
        if (characterController.isGrounded && jump)
        {
            velocity.y = Mathf.Sign(-gravity) * Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(gravity));
            //_animator.Play( Animator.StringToHash( "Jump" ) );
        }


        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = characterController.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        velocity.x = Mathf.Lerp(velocity.x, horizontalMovement * runSpeed, Time.deltaTime * smoothedMovementFactor);

        // apply gravity before moving
        velocity.y += gravity * Time.deltaTime;

        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets uf jump down through one way platforms
        if (characterController.isGrounded && dropDown)
        {
            velocity.y *= 3f;
            characterController.ignoreOneWayPlatformsThisFrame = true;
        }

        characterController.move(velocity * Time.deltaTime, gravity < 0);

        // grab our current _velocity to use as a base for all calculations
        velocity = characterController.velocity;
    }

    public void TriggeredMove(float velocityMagnitude, float angleRads)
    {
        // Trigger change in velocity using magnitude and angle
        triggeredVelocity.x = velocityMagnitude * Mathf.Cos(angleRads);
        triggeredVelocity.y = velocityMagnitude * Mathf.Sin(angleRads);

        velocity += triggeredVelocity;
        characterController.move(velocity * Time.deltaTime, gravity < 0);
    }

    public void TriggeredMove(Vector3 inputVelocity)
    {
        // Trigger change in velocity using vector
        triggeredVelocity = inputVelocity;
        velocity += triggeredVelocity;
        characterController.move(velocity * Time.deltaTime, gravity < 0);
    }

    public Vector3 GetVelocity() { return velocity; }

}
