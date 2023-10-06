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
    
    void OnTriggerEnter()
    {
        anim.Play("SludgeOpen");
    }
    void OnTriggerExit()
    {
        anim.Play("Idle");
    }
}
