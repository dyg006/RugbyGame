using DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogIn : MonoBehaviour {

    private TMP_InputField logIn;
    private Container container;
    public GameObject cross;
    public GameObject tick;
    public AudioSource correctAudio;
    public AudioSource incorrectAudio;
    public bool sceneLoad;
    
    private float mediumCalifications;


    // Use this for initialization
    void Start () {
        logIn = GetComponent<TMP_InputField>();
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
        sceneLoad = false;
	}
	
    public void Log_In()
    {
        if (this.gameObject.activeSelf == true)
        {
            // COMMENTED BECAUSE THE PAGE WHERE THE USERS ARE REGISTERED IS NOT ACTIVE ANYMORE
            //StartCoroutine(Connection.loginCoachData(logIn.text, ConnectionOk, ConnectionError));
            ConnectionOk(0.0f);
        }
        
    }

    private void ConnectionOk(float mediumCalifications)
    {
        correctAudio.Play();
        if (cross.activeSelf == true)
        {
            cross.SetActive(false);
        }

        tick.SetActive(true);
        
        this.mediumCalifications = mediumCalifications;

        PlayerPrefs.SetString("User", logIn.text);

        StartCoroutine(Connection.getVideoGameValues(logIn.text, VideogameValues, GetErrorValues));
        StartCoroutine(Connection.getVideoGameStatistics(logIn.text, StatisticsTable, GetErrorStatistics));

    }

    private void GetErrorValues()
    {
        Connection.VideogameValues[] table;
        table = new Connection.VideogameValues[10];
        Connection.VideogameValues entry;
        entry = new Connection.VideogameValues();
        entry.user = PlayerPrefs.GetString("User");
        entry.level = 1;
        entry.points = 0;
        entry.velocity = 0;
        entry.numJumps = 0;
        entry.lifes = 3;
        table[0] = entry;
        VideogameValues(table);
    }

    private void GetErrorStatistics()
    {
        Connection.VideogameStatistics[] table;
        table = new Connection.VideogameStatistics[10];
        Connection.VideogameStatistics entry;
        entry = new Connection.VideogameStatistics();
        if (entry == null)
        {
            Debug.Log("WASAAAAAAAAAAAAAAAAAAAAAAA");
        }
        else
        {
            Debug.Log("entry no es null");
        }
        entry.user = PlayerPrefs.GetString("User");
        entry.yards = 0;
        entry.acquiredLifes = 0;
        entry.timePlayedInSeconds = 0;
        table[0] = entry;
        StatisticsTable(table);
    }

    private void VideogameValues(Connection.VideogameValues[] table)
    {
        container.level = 1;
        Debug.Log("LA TABLA TIENE: " + table[0]);

           
            
        container.level = table[0].level;
        Debug.Log("ESTAS EN EL NIVEL: " + container.level); 
        container.points = table[0].points;
        container.velocity = table[0].velocity;
        container.numJumps = table[0].numJumps;
        container.lifes = table[0].lifes;
        
        if (container.level == 1)
        {
            container.initialPoints = (int)(mediumCalifications * 4);
            container.points = container.initialPoints;
            container.velocity = 0;
            container.numJumps = 0;
            container.lifes = 3;
        }
        if (sceneLoad)
            SceneManager.LoadScene("Points");
        sceneLoad = true;
    }

    private void StatisticsTable(Connection.VideogameStatistics[] table)
    {      
        container.yards = table[0].yards;
        container.acquiredLifes = table[0].acquiredLifes;
        container.timePlayed = table[0].timePlayedInSeconds;
        
        if (sceneLoad)
            SceneManager.LoadScene("Points");
        sceneLoad = true;
    }

    private void ConnectionError()
    {
        incorrectAudio.Play();
        cross.SetActive(true);
    }


}
