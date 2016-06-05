using UnityEngine;
using System.Collections;
using Prime31;
using System;

public class CloneMovement : MovementBaseClass
{
    private float _jumpHeight = 3f;
    private Transform _owner;
    private CharacterController2D _characterController;
    private Animator _animationComponent;
    private RaycastHit2D _lastControllerColliderHit;

    void Awake()
    {
        _animationComponent = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController2D>();
    }

    public void SetCloneValues(Transform owner, float gravity, float jumpHeight)
    {
        _owner = owner;
        this.gravity = gravity;
        _jumpHeight = jumpHeight;
    }

    public void OnActive(Transform owner)
    {
        // Reset Gravity
        this.gravity = Math.Abs(this.gravity);

        // Find a place to spawn
        Vector2 newPosition = new Vector2(owner.position.x, owner.position.y - owner.transform.localScale.y);

        Collider2D collision = Physics2D.OverlapCircle(newPosition, 0.25f);
        while (collision != null && collision != transform.parent)
        {
            newPosition += Vector2.down * 0.01f;
            collision = Physics2D.OverlapCircle(newPosition, 0.25f);
        }

        transform.position = new Vector2(owner.position.x, newPosition.y);
    }

    public override void Move(float horizontalMovement, bool jump, bool dropDown)
    {
        if (_characterController.isGrounded)
        {
            velocity.y = 0;
            _animationComponent.SetBool("Duck", false);
        }

        if (transform.localScale.x > 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (_characterController.isGrounded)
        {
            _animationComponent.SetBool("Jumping", false);
        }
        

        Vector3 playerVel = _owner.GetComponent<PlayerMovement>().Velocity;
        float diff = _owner.position.x - transform.position.x;

        if ((diff > 0 && playerVel.x > 0) || diff < 0 && playerVel.x < 0)
        {
            velocity.x = playerVel.x;
            _animationComponent.SetBool("Moving", Math.Abs(velocity.x) > 1.0f ? true : false);
        }
        else
        {
            velocity.x = 0;
            _animationComponent.SetBool("Moving", false);
        }

        if(Math.Abs(diff) < 0.5)
        {
            if (_characterController.isGrounded && jump && (_owner.GetComponent<CharacterController2D>().isGrounded || _owner.GetComponent<CharacterController2D>().collisionState.wasGroundedLastFrame))
            {
                velocity.y = Mathf.Sign(-gravity) * Mathf.Sqrt(2f * _jumpHeight * Mathf.Abs(gravity));
                _animationComponent.SetBool("Jumping", true);
                _animationComponent.SetBool("Moving", false);
            }

            if (_characterController.isGrounded && dropDown)
            {
                velocity.y *= 3f;
                _characterController.ignoreOneWayPlatformsThisFrame = true;
                _animationComponent.SetBool("Duck", true);
            }
        }

        // apply gravity before moving
        velocity.y += gravity * Time.deltaTime;

        if (velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (gravity > 0)
            GetComponent<SpriteRenderer>().flipY = true;
        else
            GetComponent<SpriteRenderer>().flipY = false;

        _characterController.move(velocity * Time.deltaTime, gravity < 0);

        // grab our current _velocity to use as a base for all calculations
        velocity = _characterController.velocity;
    }

    public override void TriggeredMove(float velocityMagnitude, float angleRads)
    {
        // Trigger change in velocity using magnitude and angle
        Vector3 triggeredVelocity = new Vector3();
        triggeredVelocity.x = velocityMagnitude * Mathf.Cos(angleRads);
        triggeredVelocity.y = velocityMagnitude * Mathf.Sin(angleRads);

        velocity += triggeredVelocity;
        _characterController.move(velocity * Time.deltaTime, gravity < 0);
    }

    public override void TriggeredMove(Vector3 inputVelocity)
    {
        // Trigger change in velocity using vector
        velocity += inputVelocity;
        _characterController.move(velocity * Time.deltaTime, gravity < 0);
    }

}
