using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DefenderCrashTraining : MonoBehaviour
{
    public GameObject DefenderParent;
    //public CinemachineVirtualCamera virtualCamera;
    public GameObject Spawner;
    

    void Start()
    {
        Spawner  = GameObject.FindGameObjectWithTag("SpawnerTrainer1");
    }
    
   

    void OnTriggerEnter(Collider other)
    {

        if (other.name.Contains("Bullet"))
        {
            DefenderParent.GetComponent<DefenderFollow>().explodeSequence();
            Spawner.SendMessage("killedOne");
        }
        else if (other.name.Contains("weenie"))
        {
            
            other.SendMessage("takeDamage", 2);
            DefenderParent.GetComponent<DefenderFollow>().explodeSequence();
        }
        
    }

    
}
