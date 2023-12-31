using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2Hit2 : MonoBehaviour
{
    //public 
    public Lesson2Controller lessonController;
    public GameObject vessel;
    public bool movetoVessel = false;
    public bool trainingSkipped = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //vessel = GameObject.FindGameObjectWithTag("Vessel");
    }

    // Update is called once per frame
    void Update()
    {
        if(movetoVessel == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, vessel.transform.position, 10);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            if(trainingSkipped == false)
            {
                lessonController.SendMessage("Trigger2");
            }
            Destroy(gameObject);
        }
    }

    public void moveAtVessel()
    {
        movetoVessel = true;
        trainingSkipped = true;
    }
}
