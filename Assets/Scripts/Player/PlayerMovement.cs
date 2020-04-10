using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public Vector3 incrementalForse = new Vector3(0, 0.018f, 0.1f);
    public Vector3 forceS = new Vector3(0, 0.018f, 0.1f);
    public Vector3 forceR = new Vector3(0.1f, 0, 0);
    public Vector3 forceL = new Vector3(-0.1f, 0, 0);
    public Vector3 forceJ = new Vector3(0, 1f, 0);
    public AudioSource jumpAudio;
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    public int speedPoints = 0;
    public int jumpPoints = 0;
    bool isOnTheFloor = true;
    private Container container;
    private int safe=-1;

    void Awake()
    {

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
        jumpPoints = container.numJumps;
        speedPoints = container.velocity;
    }


    void FixedUpdate()
    {

        if (SceneManager.GetActiveScene().name != "MiniGame" && (SceneManager.GetActiveScene().name != "Credits"))
        {
            safe--;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                transform.position += forceS + (speedPoints * new Vector3(0, 0, 0.01f));
                Animating(false, true, false, false);
                container.yards += 0.01f;
            }

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.position += forceR + (speedPoints * new Vector3(0.01f, 0, 0));
                Animating(false, false, true, false);
                container.yards += 0.01f;
            }

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.position += forceL + (speedPoints * new Vector3(-0.01f, 0, 0));
                Animating(false, false, false, true);
                container.yards += 0.01f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpPoints > 0 && isOnTheFloor && safe < 0)
                {
                    safe = 2;
                    jumpAudio.Play();
                    playerRigidbody.AddForce(forceJ);
                    jumpPoints--;
                }
            }
            if (!Input.anyKey)
            {
                Animating(true, false, false, false);
            }
        }
        else if (SceneManager.GetActiveScene().name == "MiniGame")
        {
            safe--;
            incrementalForse += new Vector3(0, 0.00001f, 0.001f);
            if (isOnTheFloor && safe<0)
            {
                playerRigidbody.velocity = incrementalForse;
            }
            Animating(false, true, false, false);
            if (Input.GetButtonDown("Jump") || Input.GetMouseButton(0))
            {
                if (isOnTheFloor && safe < 0)
                {
                    safe = 2;
                    jumpAudio.Play();
                    playerRigidbody.AddForce(forceJ);
                }
            }
        }

        //Caso en el que la escena sea Credits
        else
        {
            playerRigidbody.velocity = incrementalForse;
            Animating(false, true, false, false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Field")
        {
            isOnTheFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Field")
        {
            isOnTheFloor = false;
        }
    }



    void Animating(bool isIdle, bool walkingS, bool walkingR, bool walkingL)
    {

        // Tell the animator whether or not the player is walking.
        anim.SetBool("isWalkingStraight", walkingS);
        anim.SetBool("isWalkingRight", walkingR);
        anim.SetBool("isWalkingLeft", walkingL);
        anim.SetBool("isIdle", isIdle);
    }
}
