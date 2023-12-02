using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFollow : MonoBehaviour
{
    public GameObject vessel;

    // Start is called before the first frame update
    void Start()
    {
      vessel =  GameObject.FindGameObjectWithTag("Vessel");  
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Transform>().position = vessel.GetComponent<Transform>().position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Defenders"))
        {
            other.transform.parent.gameObject.SendMessage("explodeSequence");
        }
        if(other.name.Contains("Projectile"))
        {
            if(!other.GetComponent<EnemyShooterProjectileCrash>()) return;

            other.GetComponent<EnemyShooterProjectileCrash>().killProjectile();
        }
    }
}
