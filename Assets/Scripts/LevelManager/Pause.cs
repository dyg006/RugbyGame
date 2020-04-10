using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    public GameObject pauseMenu;


    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseMenu.activeSelf == false)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
