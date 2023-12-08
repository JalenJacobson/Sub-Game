using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Animator anim;
    public float Healthbar = 100;
    public bool warningInitiated = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }
    
    public void setHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        Healthbar = health;
        if(Healthbar <= 30 && warningInitiated == false)
        {
            StartCoroutine(lowLifeWarning());
        }
    }
    public IEnumerator lowLifeWarning()
    {
        anim.Play("LowLife");
        warningInitiated = true;
        yield return new WaitForSeconds(5f);
        anim.Play("RedBar");
    }


}
