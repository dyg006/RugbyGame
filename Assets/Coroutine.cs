using DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour {

    public string text;
    public Connection.getTable3 funct1;
    public Connection.getTable2 funct2;
    public Connection.errorFunction err1;
    public Connection.errorFunction err2;
    // Use this for initialization
    void Start () {
        StartCoroutine(Connection.getVideoGameValues(text, funct1, err1));
        StartCoroutine(Connection.getVideoGameStatistics(text, funct2, err2));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
