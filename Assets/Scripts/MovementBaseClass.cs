using UnityEngine;
using System.Collections;

public abstract class MovementBaseClass : MonoBehaviour {
    public float gravity;
    protected Vector3 velocity;
    public abstract void Move(float horizontalMovement, bool jump, bool dropDown);
    public Vector3 Velocity
    {
        get
        {
            return velocity;
        }
    }
}
