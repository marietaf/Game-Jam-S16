using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneManager.LoadScene("WinGameScene");
        }
    }
}
