using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {

    private GameController gc;
	
    void Start ()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

	void OnTriggerEnter2D (Collider2D col)
    {
	    if(col.CompareTag("Player") || col.CompareTag("PlayerClone") && gc != null)
        {
            Destroy(gameObject);
            gc.points++;
        }
	}
}
