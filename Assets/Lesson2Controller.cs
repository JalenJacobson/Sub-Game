using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2Controller : MonoBehaviour
{
    public SchoolLessons SchoolUI;
    public bool trigger1hit = false;
    public bool trigger2hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger1hit == true && trigger2hit == true)
        {
            SchoolUI.SendMessage("P1Lesson3");
            Destroy(gameObject);
        }
    }

    public void Trigger1()
    {
        trigger1hit = true;
    }
    public void Trigger2()
    {
        trigger2hit = true;
    }
    
}
