using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextPro : MonoBehaviour
{

    private Container container;
    private TextMeshProUGUI textPro;

    // Use this for initialization
    void Start()
    { 
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
        textPro = GetComponent<TextMeshProUGUI>();  
        textPro.text += container.level;
    }
}
