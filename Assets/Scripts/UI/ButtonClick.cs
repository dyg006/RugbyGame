using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {

    public Text text;

    public void OnMouseOver()
    {
        text.color = Color.red;  // works well
    }

    public void OnMouseExit()
    {
        text.color = Color.white;
    }
}
