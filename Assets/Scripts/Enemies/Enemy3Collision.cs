using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Collision : MonoBehaviour {


    Animator anim;

    Enemy3Movement enemyMovement;

    GameObject player;
    PlayerCollision playerCollision;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<Enemy3Movement>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollision = player.GetComponent<PlayerCollision>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            anim.Play("Placaje");
            enemyMovement.enabled = false;
        }

    }


    private void Update()
    {
        if (playerCollision.die == true || playerCollision.levelWon == true)
        {
            enemyMovement.enabled = false;
            anim.SetTrigger("PlayerDead");
        }
    }
}
