using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour {

    public Text jumps;
    public Slider slider;

	// Use this for initialization
	void Start () {
        jumps.text = slider.value.ToString();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        jumps.text = slider.value.ToString();
    }
}
