using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;
using DataBase;

public class GameManager : MonoBehaviour {

    bool gameHasEnded = false;

    public float restartDelay = 2f;

    public AudioSource victoryAudio;
    public GameObject completeLevelUI;
    public GameObject player;
    public RawImage[] rugbyLife;
    private Container container;
    public int lifes;


    private void Awake()
    {
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
        if (SceneManager.GetActiveScene().name != "MiniGame")
        {
            lifes = container.lifes;
            if (lifes == 1)
            {
                rugbyLife[1].enabled = false;
                rugbyLife[2].enabled = false;
                rugbyLife[3].enabled = false;
                rugbyLife[4].enabled = false;
            }
            else if (lifes == 2)
            {
                rugbyLife[1].enabled = true;
                rugbyLife[2].enabled = false;
                rugbyLife[3].enabled = false;
                rugbyLife[4].enabled = false;
            }
            else if (lifes == 3)
            {
                rugbyLife[1].enabled = true;
                rugbyLife[2].enabled = true;
                rugbyLife[3].enabled = false;
                rugbyLife[4].enabled = false;
            }
            else if (lifes == 4)
            {
                rugbyLife[1].enabled = true;
                rugbyLife[2].enabled = true;
                rugbyLife[3].enabled = true;
                rugbyLife[4].enabled = false;
            }
            else if (lifes == 5)
            {
                rugbyLife[1].enabled = true;
                rugbyLife[2].enabled = true;
                rugbyLife[3].enabled = true;
                rugbyLife[4].enabled = true;
            }
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                StartCoroutine(Connection.getVideoGameValues(PlayerPrefs.GetString("User"), VideogameValues, GetError));
            }
        }
    }

    public IEnumerator CompleteLevel()
    {
        if (SceneManager.GetActiveScene().name == "MiniGame")
        {
            victoryAudio.Play();

            yield return new WaitForSeconds(1f);
 
            completeLevelUI.SetActive(true);

            yield return new WaitForSeconds(2f);
            float fadeTime = this.GetComponent<Fading>().BeginFade(1);

            SceneManager.LoadScene("Menu");

            yield return new WaitForSeconds(fadeTime);

            yield return null;
        }
        else
        {
            victoryAudio.Play();
            if (SceneManager.GetActiveScene().name != "Level10" && SceneManager.GetActiveScene().name != "Credits")
            {
                container.level++;
                container.points += 3;
            }

            StartCoroutine(Connection.sendVideoGameValues(PlayerPrefs.GetString("User"), container.level, container.points, container.velocity,
            container.numJumps, container.lifes, SetOk, SetError));
            StartCoroutine(Connection.sendVideoGameStatistics(PlayerPrefs.GetString("User"), container.level, container.yards, container.velocity,
            container.numJumps, container.acquiredLifes, container.timePlayed, SetOk, SetError));

            yield return new WaitForSeconds(1f);

            completeLevelUI.SetActive(true);

            /*
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(Path.Combine(Application.streamingAssetsPath, PlayerPrefs.GetString("User") + ".csv")))
            {
                file.WriteLine("LevelReached,Yards,AcquiredSpeed,AcquiredJumps,AcquiredLifes,TimePlayedinseconds,TimePlayedinminutes");
                file.WriteLine(container.level + "," + String.Format("{0:0.00}", container.yards) + "," +
                                                container.velocity + "," + container.numJumps + "," + container.acquiredLifes
                                                + "," + String.Format("{0:0.00}", container.timePlayed) + "," + String.Format("{0:0.00}", container.timePlayed / 60));

            }
            */
            yield return new WaitForSeconds(2f);
            float fadeTime = this.GetComponent<Fading>().BeginFade(1);

            yield return new WaitForSeconds(fadeTime);

            if (SceneManager.GetActiveScene().name == "Level10")
            {
                SceneManager.LoadScene("Credits");
            }
            else if (SceneManager.GetActiveScene().name == "Credits")
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                SceneManager.LoadScene("Points");
            }
            yield return null;
        }

        
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }

    }


    void Restart()
    {
        if (SceneManager.GetActiveScene().name != "MiniGame") { 
            container.lifes = lifes;

            /*
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(Path.Combine(Application.streamingAssetsPath, PlayerPrefs.GetString("User") + ".csv")))
            {
                file.WriteLine("LevelReached,Yards,AcquiredSpeed,AcquiredJumps,AcquiredLifes,TimePlayedinseconds,TimePlayedinminutes");
                file.WriteLine(container.level + "," + String.Format("{0:0.00}", container.yards) + "," +
                                                container.velocity + "," + container.numJumps + "," + container.acquiredLifes
                                                + "," + String.Format("{0:0.00}", container.timePlayed) + "," + String.Format("{0:0.00}", container.timePlayed / 60));

            }
            */

            
            if (lifes == 0)
            {
                
                StartCoroutine(Connection.sendVideoGameValues(PlayerPrefs.GetString("User"), 1, container.initialPoints, 0,
                0, 3, SetOk, SetError));
                StartCoroutine(Connection.sendVideoGameStatistics(PlayerPrefs.GetString("User"), container.level, 0, 0,
                0, 0, container.timePlayed, SetOk, SetError));
                SceneManager.LoadScene("GameOver");
                Destroy(container.gameObject);
            }
            else {
                StartCoroutine(Connection.sendVideoGameStatistics(PlayerPrefs.GetString("User"), container.level, container.yards, container.velocity,
                container.numJumps, container.acquiredLifes, container.timePlayed, SetOk, SetError));
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            container.attempts++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void SetOk()
    {
        Debug.Log("Values established in the db");
    }

    private void SetError()
    {
        Debug.Log("An error has ocurred setting the values in the db");
    }

    private void VideogameValues(Connection.VideogameValues[] table)
    {   
        container.level = table[0].level;
        container.points = table[0].points;
        container.velocity = table[0].velocity;
        container.numJumps = table[0].numJumps;
        
    }

    private void GetError()
    {
        Debug.Log("An error has ocurred trying to get the values of the db");
    }
}
