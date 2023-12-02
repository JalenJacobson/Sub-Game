using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Shooter_TakeDamage : MonoBehaviour
{
    public GameObject parentShooter;
    public float health;

    void Start()
    {
        health = 10f;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Bullet"))
        {
            health -= 1;
            //should play take damage animation
        }
    }

    void Update()
    {
        if(health <= 0)
        {
            Destroy(parentShooter);
            //should play blow up animation
        }
    }
}
