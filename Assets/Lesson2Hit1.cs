using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2Hit1 : MonoBehaviour
{
    //public Lesson2Hit2 Hit2;
    //public GameObject Lesson2Hit2;
    public Lesson2Controller lessonController;
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
            lessonController.SendMessage("Trigger1");
            //Lesson2Hit2.SetActive(true);
            Destroy(gameObject);
        }
    }

}
