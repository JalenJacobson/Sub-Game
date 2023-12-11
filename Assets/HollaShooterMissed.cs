using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollaShooterMissed : MonoBehaviour
{
    public SchoolLessons SchoolUI;
    public GameObject ShooterHolla;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("projectile"))
        {
            SchoolUI.SendMessage("P1Complete");
            ShooterHolla.SetActive(false);
        }
    }
}
