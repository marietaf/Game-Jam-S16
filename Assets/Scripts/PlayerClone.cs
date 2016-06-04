using UnityEngine;
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
                if (clone == null)
                {
                    clone = (GameObject)Instantiate(clonePrefab, transform.position + new Vector3(0, -1.02f, 0), transform.rotation);
                    PlayerMovement move = GetComponent<PlayerMovement>();
                    clone.GetComponent<CloneMovement>().SetCloneValues(GetComponent<Transform>(), move.gravity, move.runSpeed, move.groundDamping, move.inAirDamping, move.jumpHeight);
                }
                clone.SetActive(true);
                //clone.GetComponent<CloneMovement>().gravity = GetComponent<PlayerMovement>().gravity;
            } else
            {
                isCloneInstantiated = false;
                clone.SetActive(false);
            }
        }
    }
}
