using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    Transform player;
    Animator anim;
    NavMeshAgent nav;
    Container container;
    int flagPlayerBehind=0;


	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.updateRotation = false;
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
        nav.speed = 1 + (0.5f * container.level);
        if(nav.speed > 5)
        {
            nav.speed = 5;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (nav.enabled == true)
        {
            if (player.transform.position.y > transform.position.y && player.transform.position.z > transform.position.z && flagPlayerBehind==0)
            {
                anim.SetTrigger("PlayerIsBehind");
                flagPlayerBehind = 1;
            }
            nav.SetDestination(player.position);
        }
    }
}
