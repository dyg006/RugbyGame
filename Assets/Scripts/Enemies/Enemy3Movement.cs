using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy3Movement : MonoBehaviour {


    public enum Animations
    {
        CorrerAtras,
        CorrerAdelante,
        Placaje,
        Fijo
    };
    Animator anim;
    Container container;
    public Animations animations;
    int contador;

    void Awake()
    {
        anim = GetComponent<Animator>();
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
        
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
            if (container.level >= 8)
            {
                if (animations == Animations.CorrerAtras)
                {
                    transform.position += new Vector3(0, 0, -0.1f);
                }
                if (animations == Animations.CorrerAdelante)
                {
                    transform.position += new Vector3(0, 0, 0.1f);
                }
                contador++;
                if (contador >= 100)
                {
                    ChangeDirection();
                    contador = 0;
                }
            }
            else
            {
                if (animations == Animations.CorrerAtras)
                {
                    transform.position += new Vector3(0, 0, -0.05f);
                }
                if (animations == Animations.CorrerAdelante)
                {
                    transform.position += new Vector3(0, 0, 0.05f);
                }
                contador++;
                if (contador >= 100)
                {
                    ChangeDirection();
                    contador = 0;
                }
            }
        }
    }

    private void ChangeDirection()
    {
        if (animations == Animations.CorrerAdelante)
        {
            anim.SetTrigger("changeDirection");
            animations = Animations.CorrerAtras;
        }

        else if (animations == Animations.CorrerAtras)
        {
            anim.SetTrigger("changeDirection");
            animations = Animations.CorrerAdelante;
        }
    }
}
