using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    private Container container;

    public void PlayGame()
    {

        
        if(GameObject.FindGameObjectWithTag("Container") != null)
        {
            container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
            SceneManager.LoadScene("Level" + container.level);
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MiniGame()
    {
        SceneManager.LoadScene("MiniGame");
    }


    
}
