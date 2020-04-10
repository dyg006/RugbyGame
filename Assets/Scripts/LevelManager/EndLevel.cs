using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{

    public GameManager gameManager;
    // Use this for initialization
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
           StartCoroutine( gameManager.CompleteLevel());
        }

    }

}
