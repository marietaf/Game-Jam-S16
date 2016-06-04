using UnityEngine;
using System.Collections;

public abstract class InputBaseClass : MonoBehaviour
{

    //// Use this for initialization
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    
    public abstract float GetHorizontalMovement();
    public abstract bool GetJump();
    public abstract bool GetDropDown();
}
