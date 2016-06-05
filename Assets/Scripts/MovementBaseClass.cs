using UnityEngine;
using System.Collections;

public abstract class MovementBaseClass : MonoBehaviour {
    public float gravity;
    protected Vector3 velocity;
    public abstract void Move(float horizontalMovement, bool jump, bool dropDown);
    public abstract void TriggeredMove(float velocityMagnitude, float angleRads);
    public abstract void TriggeredMove(Vector3 inputVelocity);
    public Vector3 Velocity
    {
        get
        {
            return velocity;
        }
    }
}
