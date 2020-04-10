using DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour {

    private Container container;
    public Text points;
    public Text velocity;
    public Text lifes;
    public Text numJumps;

	// Use this for initialization
	void Start () {
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
    }

    public void NextLevelFunct()
    {
        container.points = int.Parse(points.text);
        container.velocity = int.Parse(velocity.text);
        if(int.Parse(lifes.text) > container.lifes)
        {
            container.acquiredLifes += int.Parse(lifes.text) - container.lifes;
        }
        container.lifes = int.Parse(lifes.text);
        container.numJumps = int.Parse(numJumps.text);

        StartCoroutine(Connection.sendVideoGameValues(PlayerPrefs.GetString("User"), container.level, container.points, container.velocity,
            container.numJumps, container.lifes, SetOk, SetError));
        StartCoroutine(Connection.sendVideoGameStatistics(PlayerPrefs.GetString("User"), container.level, container.yards, container.velocity,
        container.numJumps, container.acquiredLifes, container.timePlayed, SetOk, SetError));
        SceneManager.LoadScene("Level" + container.level);
    }

    private void SetOk()
    {
        Debug.Log("Values established in the db");
    }

    private void SetError()
    {
        Debug.Log("An error has ocurred setting the values in the db");
    }
}
