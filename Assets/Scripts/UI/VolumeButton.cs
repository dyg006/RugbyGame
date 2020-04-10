using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeButton : MonoBehaviour {

    public GameObject onButton;
    public GameObject offButton;

	// Use this for initialization
	void Start () {
        if (AudioListener.volume == 0f)
        {
            onButton.SetActive(false);
            offButton.SetActive(true);
        }
        else
        {
            onButton.SetActive(true);
            offButton.SetActive(false);
        }
	}
	
}
