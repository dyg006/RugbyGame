using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCount : MonoBehaviour {

    public Text points;
    private Container container;
    public Slider velocitySlider;
    public Slider lifesSlider;
    public Slider jumpSlider;
    private float velocityValueBefore = 0f;
    private float lifesValueBefore = 0f;
    private float jumpsValueBefore = 0f;
    private float auxJumpSlider = 0;
    private float auxVelocitySlider = 0;
    private float auxLifesSlider = 0;
    private String auxpoints = "";
    private float velocityFirstValue;
    private float lifesFirstValue;
    private float jumpsFirstValue;

    // Use this for initialization
    void Start () {
        container = GameObject.FindGameObjectWithTag("Container").GetComponent<Container>();
        points.text = container.points.ToString();
        lifesSlider.value = container.lifes;
        velocitySlider.value = container.velocity;
        jumpSlider.value = container.numJumps;
        velocitySlider.onValueChanged.AddListener(delegate { ValueChangeCheckVelocity(); });
        lifesSlider.onValueChanged.AddListener(delegate { ValueChangeCheckLifes(); });
        jumpSlider.onValueChanged.AddListener(delegate { ValueChangeCheckJumps(); });
        velocityValueBefore = velocitySlider.value;
        lifesValueBefore = lifesSlider.value;
        jumpsValueBefore = jumpSlider.value;
        velocityFirstValue = velocitySlider.value;
        lifesFirstValue = lifesSlider.value;
        jumpsFirstValue = jumpSlider.value;

    }

    public void ValueChangeCheckVelocity()
    {
        float x = float.Parse(points.text);
        x -= velocitySlider.value - velocityValueBefore;
        points.text = x.ToString();
        velocityValueBefore = velocitySlider.value;
    }

    
    public void ValueChangeCheckLifes()
    {
        float x = float.Parse(points.text);
        x -= (lifesSlider.value - lifesValueBefore)* 2;
        points.text = x.ToString();
        lifesValueBefore = lifesSlider.value;
    }

    public void ValueChangeCheckJumps()
    {
        float x = float.Parse(points.text);
        x -= (jumpSlider.value - jumpsValueBefore) * 3;
        points.text = x.ToString();
        jumpsValueBefore = jumpSlider.value; 
    }

    // This two functions are to block the slider if you try to get negative points
    public void LateUpdate () {
        auxVelocitySlider = velocitySlider.value;
        auxLifesSlider = lifesSlider.value;
        auxJumpSlider = jumpSlider.value;
        auxpoints = points.text;
    }

    private void Update()
    {
        if(float.Parse(points.text) < 0 || velocitySlider.value < velocityFirstValue || lifesSlider.value < lifesFirstValue || jumpSlider.value < jumpsFirstValue)
        {
            velocitySlider.value = auxVelocitySlider;
            lifesSlider.value = auxLifesSlider;
            jumpSlider.value = auxJumpSlider;
            points.text = auxpoints;
        }
    }
    
}
