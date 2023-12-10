using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;
    public WeenieArmy WeenieArmy;
    public bool doorReady = false;
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
        if(other.name.Contains("Bullet") && doorReady == true)
        {
           anim.Play("OpenDoor");
           WeenieArmy.SendMessage("doorOpened");
        }
    }

    public void beginJourney()
    {
        if(doorReady == false)
        {
            doorReady = true;
            anim.Play("Activated");

        }
    }
}
