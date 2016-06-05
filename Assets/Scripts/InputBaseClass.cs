using UnityEngine;
using System.Collections;

public abstract class InputBaseClass : MonoBehaviour
{   
    public abstract float GetHorizontalMovement();
    public abstract bool GetJump();
    public abstract bool GetDropDown();
    public abstract bool GetToggleClone();
}
