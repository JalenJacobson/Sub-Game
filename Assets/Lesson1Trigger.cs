using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1Trigger : MonoBehaviour
{
    public SchoolLessons SchoolUI;
    public Lesson2Hit1 Lesson2;
    public GameObject Lesson2Hit1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            SchoolUI.SendMessage("P1Lesson2");
            Lesson2Hit1.SetActive(true);
            Destroy(gameObject);
        }
    }
}
