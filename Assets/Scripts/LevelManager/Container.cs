using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {

    public int lifes = 3;
    public int acquiredLifes = 0;
    public int points = 0;
    public int initialPoints = 0;
    public int numJumps = 0;
    public int velocity = 0;
    public int level = 1;
    public int attempts = 1;
    public float yards = 0;
    public float timePlayed = 0;

    private static Container instance;
	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Update()
    {
        timePlayed += Time.deltaTime;
    }
}
