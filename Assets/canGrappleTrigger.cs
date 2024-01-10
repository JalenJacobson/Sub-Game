using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canGrappleTrigger : MonoBehaviour
{
    public JumpPointer JumpPointer_Script;

    void Start()
    {
        JumpPointer_Script = GameObject.Find("Pointer").GetComponent<JumpPointer>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Grapple")
        {
            JumpPointer_Script.inRangeGrapple = true;
            other.SendMessage("canGrapple");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Grapple")
        {
            JumpPointer_Script.inRangeGrapple = false;
            other.SendMessage("cantGrapple");
        }
    }
}
