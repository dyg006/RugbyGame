using DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {

    public GameObject pointsMenu;
    public GameObject rankingMenu;
    public Transform fatherObject;
    public GameObject tableRow;

    public Text[][] tableText;

    private string[] users;
    private int[] levelReached;
    private float[] yards;
    private int[] acquiredSpeed;
    private int[] acquiredJumps;
    private int[] acquiredLifes;
    private float[] timePlayedInSeconds;
    private float[] timePlayedInMinutes;

    

	// Use this for initialization
	void Start () {
        StartCoroutine(Connection.getAllVideogameStatistics(PlayerPrefs.GetString("User"), StatisticsTable, GetError));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ShowRanking()
    {
        
    }


    public void RankingButton()
    {
        if (pointsMenu.gameObject.activeSelf)
        {
            pointsMenu.SetActive(false);
            rankingMenu.SetActive(true);
        }
        else
        {
            rankingMenu.SetActive(false);
            pointsMenu.SetActive(true);
        }
       
    }

    private void StatisticsTable(Connection.VideogameStatistics[] table)
    {
        Debug.Log("El entry tiene: " + table[0].levelReached.ToString());
        foreach(var entry in table)
        {
            Debug.Log("table tiene " + table.Length + "elementos");
            GameObject row = Instantiate(tableRow, fatherObject);
            row.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = entry.user;
            row.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = entry.levelReached.ToString();
            row.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = entry.yards.ToString();
            row.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = entry.acquiredSpeed.ToString();
            row.transform.GetChild(4).GetComponent<UnityEngine.UI.Text>().text = entry.acquiredJumps.ToString();
            row.transform.GetChild(5).GetComponent<UnityEngine.UI.Text>().text = entry.acquiredLifes.ToString();
            row.transform.GetChild(6).GetComponent<UnityEngine.UI.Text>().text = entry.timePlayedInSeconds.ToString();
            row.transform.GetChild(7).GetComponent<UnityEngine.UI.Text>().text = entry.timePlayedInMinutes.ToString();
            row.SetActive(true);
        } 
        
    }

    private void GetError()
    {
        Debug.Log("An error has ocurred trying to get data from the db");
    }
}
