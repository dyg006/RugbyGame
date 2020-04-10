using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour {

    private Container container;
    private Text text;

	// Use this for initialization
	void Start () {
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
        text = GetComponent<Text>();
        text.text += container.level.ToString();
    }
}
