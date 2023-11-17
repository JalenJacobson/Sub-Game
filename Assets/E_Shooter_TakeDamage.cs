using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Shooter_TakeDamage : MonoBehaviour
{
    public GameObject parentShooter;

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Bullet"))
        {
            Destroy(parentShooter);
        }
    }
}
