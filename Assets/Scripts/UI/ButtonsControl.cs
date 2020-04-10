using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsControl : MonoBehaviour {


	public void HomeButton ()
    {
        SceneManager.LoadScene("Menu");
	}
	

	public void TryAgainButton ()
    {
        SceneManager.LoadScene("Level1");
    }

}
