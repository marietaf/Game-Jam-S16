using UnityEngine;
using System.Collections;

public class Player2Input : InputBaseClass {

    public override float GetHorizontalMovement()
    {
        //if both or neither key is pressed, return 0
        if (!(Input.GetKey(KeyCode.LeftArrow) ^ Input.GetKey(KeyCode.RightArrow)))
        {
            return 0f;
        }
        else
        {
            //if left is pressed return -1, if not right is pressed so return 1
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return -1f;
            }
            else
            {
                return 1f;
            }
        }
    }

    public override bool GetJump()
    {

        return Input.GetKeyDown(KeyCode.UpArrow);
    }

    public override bool GetDropDown()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }

    public override bool GetToggleClone()
    {
        return Input.GetKeyDown(KeyCode.Slash);
    }
}
