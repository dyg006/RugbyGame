using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Transform player;
    public Text scoreText;
    public Text highscoreText;
    public Text attemptsText;
    private Container container;

    private void Awake()
    {
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
    }

    private void FixedUpdate()
    {
        scoreText.text = player.position.z.ToString("0");
        if (PlayerPrefs.GetInt("highscore") > Int32.Parse(highscoreText.text))
        {
            highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
        }
        
        attemptsText.text = container.attempts.ToString();
    }

    public void ChangeHighscore()
    {
        if((int)player.position.z > Int32.Parse(highscoreText.text))
        {
            PlayerPrefs.SetInt("highscore", (int)player.position.z);
        }
       
    }
}
