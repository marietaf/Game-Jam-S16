﻿using UnityEngine;
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

    void OnTriggerEnter2D (Collider2D entering) {

        if(_cols.Count > 0)
        {
            PlayerMovement enterMovement, exitMovement;
            foreach (Collider2D col in _cols)
            {
                enterMovement = entering.GetComponent<PlayerMovement>();
                exitMovement = col.GetComponent<PlayerMovement>();

                Vector2 transferVelocity = enterMovement.GetVelocity();

                exitMovement.TriggeredMove(new Vector2(0, transferVelocity.y));
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
