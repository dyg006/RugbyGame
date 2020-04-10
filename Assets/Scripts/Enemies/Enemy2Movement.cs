using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2Movement : MonoBehaviour
{

    public enum Animations
    {
        CorrerDerecha,
        CorrerIzquierda,
        Placaje,
        Fijo
    };
    Animator anim;
    Container container;
    public Animations animations;

    void Awake()
    {
        anim = GetComponent<Animator>();
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
        if (animations == Animations.CorrerIzquierda)
        {
            anim.SetTrigger("WallCollision");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Credits")
        {
            anim.SetTrigger("PlayerDead");
        }
        else
        {
            if (animations == Animations.CorrerDerecha)
            {
                if (container.level >= 5)
                {
                    transform.position += new Vector3(0.2f, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(0.15f, 0, 0);
                }
            }
            if (animations == Animations.CorrerIzquierda)
            {
                if (container.level >= 5)
                {
                    transform.position += new Vector3(-0.2f, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(-0.15f, 0, 0);
                }
            }
        }
    }

    public void ChangeDirection()
    {
        if (transform.position.x > 1.0f)
        {
            animations = Animations.CorrerIzquierda;
        }
        else
        {
            animations = Animations.CorrerDerecha;
        }
    }
}
