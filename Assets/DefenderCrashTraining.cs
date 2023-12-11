using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DefenderCrashTraining : MonoBehaviour
{
    public GameObject DefenderParent;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject SpawnerPref;
    
    //public Spawner_DTraining spawner;

    void Start()
    {
        //SpawnerPref = GameObject.FindGameObjectsWithTag("SpawnerTraining1");
    }
    
   

    void OnTriggerEnter(Collider other)
    {

        if (other.name.Contains("Bullet"))
        {
            DefenderParent.GetComponent<DefenderFollow>().explodeSequence();
            SpawnerPref.SendMessage("killedOne");
        }
        else if (other.name.Contains("weenie"))
        {
            
            other.SendMessage("takeDamage", 2);
            DefenderParent.GetComponent<DefenderFollow>().explodeSequence();
        }
        
    }

    
}
