using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollaShooterMissed : MonoBehaviour
{
    public int misses = 3;
    public SchoolLessons SchoolUI;
    public GameObject ShooterHolla;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(misses <= 0)
        {
            SchoolUI.SendMessage("P1Complete");
            ShooterHolla.SetActive(false);
            Destroy(gameObject);
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("projectile"))
        {
            misses -= 1;
        }
    }
}
