using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public float GetHorizontalMovement()
    {
        //if both or neither key is pressed, return 0
        if(!((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) ^ (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))))
        {
            return 0f;
        }
        else
        {
            //if left is pressed return -1, if not right is pressed so return 1
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                return -1f;
            }
            else
            {
                return 1f;
            }
        }
    }

    public bool GetJump()
    {

        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetDropDown()
    {
        return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
    }

    public bool GetToggleClone()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
