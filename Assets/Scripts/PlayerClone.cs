using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerClone : MonoBehaviour {

    public GameObject clonePrefab;

    [HideInInspector]
    public bool isCloneActive;
    [HideInInspector]
    public GameObject clone;

    public void Start()
    {
        isCloneActive = false;
    }

	public void ToggleClone (bool togglePressed) {
        if(togglePressed)
        {
            if (!isCloneActive) {
                isCloneActive = true;
                if (clone == null)
                {
                    clone = (GameObject)Instantiate(clonePrefab, transform.position + new Vector3(0, -1.02f, 0), transform.rotation);
                    PlayerMovement move = GetComponent<PlayerMovement>();
                    clone.GetComponent<CloneMovement>().SetCloneValues(GetComponent<Transform>(), -move.gravity, move.runSpeed, move.groundDamping, move.inAirDamping, move.jumpHeight);
                }
                clone.SetActive(true);
            } else
            {
                isCloneActive = false;
                clone.SetActive(false);
            }
        }
    }
}
