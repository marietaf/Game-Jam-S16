using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public int points;
    public Text pointsText;
	
	void Update () {
        pointsText.text = "Coins: " + points;
	}
}
