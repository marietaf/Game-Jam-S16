using UnityEngine;
using System.Collections;
using Prime31;
using System;

public class CloneMovement : MovementBaseClass
{
    public float syncTolerance = .1f;

    //float gravity = -25f;
    float runSpeed = 8f;
    float groundDamping = 20f; // how fast do we change direction? higher means faster
    float inAirDamping = 5f;
    float jumpHeight = 3f;

    [SerializeField]
    private bool isSynced = false;
    bool tooFarLeft = false;
    int syncInFrames = -1;

    private Transform owner;
    private CharacterController2D characterController;
    //private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    //private Vector3 velocity; 

    void Awake()
    {
        //_animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController2D>();
    }

    public void SetCloneValues(Transform owner, float gravity, float runSpeed, float groundDamping, float inAirDamping, float jumpHeight)
    {
        this.owner = owner;
        this.gravity = gravity;
        this.runSpeed = runSpeed;
        this.groundDamping = groundDamping;
        this.inAirDamping = inAirDamping;
        this.jumpHeight = jumpHeight;
    }

    public override void Move(float horizontalMovement, bool jump, bool dropDown)
    {
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

        if (isSynced)
        {
            if (transform.position.x + syncTolerance < owner.position.x)
            {
                Debug.Log("1");
                isSynced = false;
                tooFarLeft = true;
            }
            else if(transform.position.x - syncTolerance > owner.position.x)
            {
                Debug.Log("2");
                isSynced = false;
                tooFarLeft = false;
            }
        }
        else
        {
            if ((transform.position.x > owner.position.x && tooFarLeft) || (transform.position.x < owner.position.x && !tooFarLeft))
            {
                Debug.Log("moving to : " + new Vector2(owner.position.x, transform.position.y));
                GetComponent<Rigidbody2D>().position = new Vector2(owner.position.x, transform.position.y);
                Debug.Log("Position : " + new Vector2(transform.position.x, transform.position.y));
                isSynced = true;
                syncInFrames = 30;
            } 
        }

        if (syncInFrames == 0 || Input.GetKeyDown(KeyCode.Space))
        {
            syncInFrames = -1;
            Debug.Log("moving to : " + new Vector2(owner.position.x, transform.position.y));
            transform.position = new Vector2(owner.position.x, transform.position.y);
            Debug.Log("Position : " + new Vector2(transform.position.x, transform.position.y));
        }
        else if (syncInFrames > 0)
        {
            syncInFrames--;
        }
        
        if (!isSynced)
        {
            velocity.x = 0;

        }
        else
        {
            
            // we can only jump whilst grounded
            if (characterController.isGrounded && jump && (owner.GetComponent<CharacterController2D>().isGrounded || owner.GetComponent<CharacterController2D>().collisionState.wasGroundedLastFrame))
            {
                velocity.y = Mathf.Sign(-gravity) * Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(gravity));
                //_animator.Play( Animator.StringToHash( "Jump" ) );
            }


            // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
            //var smoothedMovementFactor = characterController.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
            //velocity.x = Mathf.Lerp(velocity.x, horizontalMovement * runSpeed, Time.deltaTime * smoothedMovementFactor);
            velocity.x = owner.GetComponent<PlayerMovement>().Velocity.x;
            //lets try and sync 
            
            //Debug.Log(transform.position.x - owner.position.x);
            //if (Mathf.Abs(transform.position.x - owner.position.x) > perfectSyncTolerance)
            //{
            //    if (transform.position.x > owner.position.x)
            //    {
            //        //velocity.x -= 1f;
            //    }
            //    else
            //    {
            //        //velocity.x += 1f;
            //    }
            //}
            // if holding down bump up our movement amount and turn off one way platform detection for a frame.
            // this lets uf jump down through one way platforms
            if (characterController.isGrounded && dropDown)
            {
                Debug.Log("drop!");
                velocity.y *= 3f;
                characterController.ignoreOneWayPlatformsThisFrame = true;
            }
        }

        // apply gravity before moving
        velocity.y += gravity * Time.deltaTime;

        characterController.move(velocity * Time.deltaTime, gravity < 0);

        // grab our current _velocity to use as a base for all calculations
        velocity = characterController.velocity;
    }
    
}
