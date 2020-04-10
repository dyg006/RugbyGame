using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {

    public GameObject onText;
    public GameObject offText;

    public void SoundButtonClick()
    {
        if (onText.activeInHierarchy == true)
        {
            onText.SetActive(false);
            offText.SetActive(true);
            AudioListener.volume = 0f;
        }
        else
        {
            onText.SetActive(true);
            offText.SetActive(false);
            AudioListener.volume = 1f;
        }
    }
}
