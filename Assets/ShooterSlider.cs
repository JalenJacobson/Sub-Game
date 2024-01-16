using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterSlider : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image sliderImg;
    public float Healthbar = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setHealth(float health)
    {
        fill.enabled = true;
        sliderImg.enabled = true;
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        Healthbar = health;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
