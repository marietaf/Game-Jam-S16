﻿using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerMovement : MovementBaseClass {

    // movement config
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;
    
    private CharacterController2D characterController;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 triggeredVelocity;
    private Animator animationComponent;

    void Awake() 
    {
        animationComponent = GetComponent<Animator>();
        characterController = GetComponent<CharacterController2D>();

        // listen to some events for debug purposes
        characterController.onControllerCollidedEvent += onControllerCollider;
        //characterController.onTriggerEnterEvent += onTriggerEnterEvent;
        //characterController.onTriggerExitEvent += onTriggerExitEvent;
    }

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if ((hit.normal.y == 1f && gravity < 0) || (hit.normal.y == -1f && gravity > 0))
        {
            return;
        }

        // logs any collider hits if uncommented.
        //Debug.Log( "flags: " + characterController.collisionState + ", hit.normal: " + hit.normal );
    }

    void Start () {
	
	}
	
	public override void Move(float horizontalMovement, bool jump, bool dropDown) {
        if (characterController.isGrounded)
        {
            velocity.y = 0;
            animationComponent.SetBool("Duck", false);
        }

        if (transform.localScale.x > 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (characterController.isGrounded)
        {
            animationComponent.SetBool("Jumping", false);
            if (horizontalMovement == 0)
            {
                animationComponent.SetBool("Moving", false);

            }
            else
            {
                animationComponent.SetBool("Moving", true);
            }
        }

        // we can only jump whilst grounded
        if (characterController.isGrounded && jump)
        {
            velocity.y = Mathf.Sign(-gravity) * Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(gravity));
            animationComponent.SetBool("Jumping", true);
            animationComponent.SetBool("Moving", false);
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
            animationComponent.SetBool("Duck", true);
            velocity.y *= 3f;
            characterController.ignoreOneWayPlatformsThisFrame = true;
        }

        characterController.move(velocity * Time.deltaTime, gravity < 0);

        if (velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (gravity > 0)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
        }

        velocity = characterController.velocity;
    }

    public override void TriggeredMove(float velocityMagnitude, float angleRads)
    {
        // Trigger change in velocity using magnitude and angle
        triggeredVelocity.x = velocityMagnitude * Mathf.Cos(angleRads);
        triggeredVelocity.y = velocityMagnitude * Mathf.Sin(angleRads);

        velocity += triggeredVelocity;
        characterController.move(velocity * Time.deltaTime, gravity < 0);
    }

    public override void TriggeredMove(Vector3 inputVelocity)
    {
        // Trigger change in velocity using vector
        velocity += inputVelocity;
        characterController.move(velocity * Time.deltaTime, gravity < 0);
        //Debug.Log(velocity.ToString());
    }

}
