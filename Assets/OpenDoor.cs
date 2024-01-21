using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;
    public bool doorReady = true;
    public SwimSchool SwimSchool;
    public GameObject P2Completed_ui;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Grenade") && doorReady == true)
        {
           anim.Play("OpenDoor");
           P2Completed_ui.SetActive(false);
        }
    }

    public void beginJourney()
    {
        if(doorReady == false)
        {
            doorReady = true;
            anim.Play("Activated");
            SwimSchool.SendMessage("destroySchool");

        }
    }
}
