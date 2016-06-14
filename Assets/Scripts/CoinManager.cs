using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {

    private GameController gameController;
	
    void Start ()
    {
        //we could also put a ref to the gameController on the players, or track this stat in the players so we know 
        //who collected the conins and we wont be makeing hundreds of calls to findgameobject
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

	void OnTriggerEnter2D (Collider2D col)
    {
	    if(col.CompareTag("Player") || col.CompareTag("PlayerClone"))
        {
            Destroy(gameObject);
            gameController.points++;
        }
	}
}
