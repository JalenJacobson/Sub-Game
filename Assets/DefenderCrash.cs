using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DefenderCrash : MonoBehaviour
{
    public GameObject DefenderParent;
    //public CinemachineVirtualCamera virtualCamera;
    
   

    void OnTriggerEnter(Collider other)
    {

        if (other.name.Contains("Bullet"))
        {
            DefenderParent.GetComponent<DefenderFollow>().explodeSequence();
        }
        else if (other.name.Contains("weenie"))
        {
            
            other.SendMessage("takeDamage", 2);
            DefenderParent.GetComponent<DefenderFollow>().explodeSequence();
        }
        
    }

    
}
