﻿using UnityEngine;
using System.Collections;
using System;

public class PlayerInput : InputBaseClass {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public override float GetHorizontalMovement()
    {
        //if both or neither key is pressed, return 0
        if(!( Input.GetKey(KeyCode.A) ^  Input.GetKey(KeyCode.D)))
        {
            return 0f;
        }
        else
        {
            //if left is pressed return -1, if not right is pressed so return 1
            if (Input.GetKey(KeyCode.A))
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

        return Input.GetKeyDown(KeyCode.W);
    }

    public override bool GetDropDown()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
}
