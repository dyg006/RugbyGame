using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;

    Animator anim;

    //CapsuleCollider playerCollider;
    int contador = 0;

    public AudioSource collisionSound;
    public bool die = false;
    public bool levelWon = false;
    public GameManager gameManager;
    public Score score;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Enemy")
        {
            if (SceneManager.GetActiveScene().name != "MiniGame")
            {
                movement.enabled = false;
                anim.Play("Muerte");
                die = true;
                gameManager.lifes--;
                collisionSound.Play();
                gameManager.EndGame();
            }
            else
            {
                score.ChangeHighscore();
                movement.enabled = false;
                anim.Play("Muerte");
                die = true;
                collisionSound.Play();
                gameManager.EndGame();
            }
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            contador++;
            if (contador == 2)
            {
                movement.enabled = false;
                anim.Play("Victoria");
                levelWon = true;
            }
        }
    }

}

    