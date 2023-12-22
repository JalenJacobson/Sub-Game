using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canGrappleTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("grapple"))
        {
            other.SendMessage("canGrapple");
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name.Contains("grapple"))
        {
            other.SendMessage("cantGrapple");

        }
    }
}
