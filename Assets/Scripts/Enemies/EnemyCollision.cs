using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollision : MonoBehaviour {

    Animator anim;

    NavMeshAgent nav;

    EnemyMovement enemyMovement;

    GameObject player;
    PlayerCollision playerCollision;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        enemyMovement = GetComponent<EnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollision = player.GetComponent<PlayerCollision>();
        nav.enabled = false;
	}

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            anim.Play("Placaje");
            nav.enabled = false;
            enemyMovement.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerRange")
        {
            nav.enabled = true;
            anim.SetTrigger("PlayerIsInRange");
        }
    }

    private void Update()
    {
        if (playerCollision.die == true  || playerCollision.levelWon==true)
        {
            nav.enabled = false;
            enemyMovement.enabled = false;
            anim.SetTrigger("PlayerDead");
        }
    }
}
