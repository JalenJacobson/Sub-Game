using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2Hit2 : MonoBehaviour
{
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
            lessonController.SendMessage("Trigger2");
            Destroy(gameObject);
        }
    }
}
