using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterProjectileCrash : MonoBehaviour
{
    public GameObject parentProjectile;

    void OnTriggerEnter(Collider other)
    {
        // if (other.name.Contains("Bullet"))
        // {
        //     parentProjectile.GetComponent<EnemyShooterProjectile>().dead = true;
        //     gameObject.GetComponent<Collider>().enabled=false;
        
        
        //     killProjectile();
        // }

        if (other.name.Contains("weenie"))
        {
            // StopAllCoroutines();
            // gameObject.GetComponent<Collider>().enabled=false;
            // attackMode = false;
            // anim.Play("DefenderExplode");
            other.SendMessage("takeDamage", 10);
            killProjectile();
            
        }
        else if(other.name.Contains("Bullet") || other.name.Contains("Env") || other.name.Contains("walls") || other.name.Contains("cave") || other.name.Contains("Door") || other.name.Contains("Spine") || other.name.Contains("Mountain") || other.name.Contains("Vein"))
        {
            parentProjectile.GetComponent<EnemyShooterProjectile>().dead = true;
            gameObject.GetComponent<Collider>().enabled=false;
            killProjectile();
        }

        
    }

    public void killProjectile()
    {
        Destroy(parentProjectile);        
    }
}
