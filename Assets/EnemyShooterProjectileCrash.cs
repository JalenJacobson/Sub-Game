using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterProjectileCrash : MonoBehaviour
{
    public GameObject parentProjectile;

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Bullet"))
        {
            parentProjectile.GetComponent<EnemyShooterProjectile>().dead = true;
            gameObject.GetComponent<Collider>().enabled=false;
        
        
            killProjectile();
        }

        else if (other.name.Contains("weenie"))
        {
            // StopAllCoroutines();
            // gameObject.GetComponent<Collider>().enabled=false;
            // attackMode = false;
            // anim.Play("DefenderExplode");
            other.SendMessage("takeDamage", 10);
            print("hit the weenie");
            killProjectile();
            
        }

        
    }

    public void killProjectile()
    {
        Destroy(parentProjectile);        
    }
}
