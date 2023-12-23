using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slugTrigger : MonoBehaviour
{

    public GameObject Sludge;
    public Animator anim;

    void Start()
    {

        anim = Sludge.GetComponent<Animator>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            anim.Play("SludgeOpen");   
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            anim.Play("Idle");
        }
    }
}
