﻿using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerClone : MonoBehaviour {

    public GameObject clonePrefab;

    private bool isCloneInstantiated;
    GameObject clone;

    public void Start()
    {
        isCloneInstantiated = false;
    }

	public void ToggleClone (bool togglePressed) {
        if(togglePressed)
        {
            if (!isCloneInstantiated) {
                isCloneInstantiated = true;
                if(clone == null)
                    clone = (GameObject)Instantiate(clonePrefab, transform.position + Vector3.down, transform.rotation);
                clone.SetActive(true);
            } else
            {
                isCloneInstantiated = false;
                clone.SetActive(false);
            }
        }
    }
}