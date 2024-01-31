using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2Hit1 : MonoBehaviour
{
    //public Lesson2Hit2 Hit2;
    //public GameObject Lesson2Hit2;
    public Lesson2Controller lessonController;
    public GameObject vessel;
    public bool movetoVessel = false;
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
            transform.position = Vector3.MoveTowards(transform.position, vessel.transform.position, 500 * Time.deltaTime);
        }
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

    public void moveAtVessel()
    {
        movetoVessel = true;
    }

}
