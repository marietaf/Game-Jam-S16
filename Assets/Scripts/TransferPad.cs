using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class TransferPad: MonoBehaviour {

    /// <summary>
    /// Velocity/momentum transfer system requiring 2 players/characters.
    /// </summary>
    private List<Collider2D> _cols;

    void Start () {
        _cols = new List<Collider2D>();
    }

    void Update()
    {
        foreach (Collider2D col in _cols)
        {
            if(col.GetComponent<CloneMovement>() != null && !col.gameObject.activeSelf)
            {
                _cols.Remove(col);
            }
        }
    }

    void OnTriggerEnter2D (Collider2D entering) {

        if(_cols.Count > 0)
        {
            MovementBaseClass enterMovement, exitMovement;
            foreach (Collider2D col in _cols)
            {
                enterMovement = (entering.GetComponent<PlayerMovement>() != null) ? entering.GetComponent<PlayerMovement>() : (MovementBaseClass)entering.GetComponent<CloneMovement>();
                exitMovement = (col.GetComponent<PlayerMovement>() != null) ? col.GetComponent<PlayerMovement>() : (MovementBaseClass)col.GetComponent<CloneMovement>();

                Vector2 transferVelocity = enterMovement.Velocity;

                exitMovement.TriggeredMove(new Vector2(0, transferVelocity.y * 1.05f));
            }
        }

        _cols.Add(entering);
    }

    
    void OnTriggerExit2D(Collider2D exiting)
    {
        if(_cols.Contains(exiting))
            _cols.Remove(exiting);
    }

}
