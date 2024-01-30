using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFollow : MonoBehaviour
{
    public GameObject vessel;
    public MeshRenderer shieldVisible;
    public float shieldHealth = 20;
    public GameObject LifeBar;
    public LifeBar lifebar_script;

    // Start is called before the first frame update
    void Start()
    {
      vessel =  GameObject.FindGameObjectWithTag("Vessel");  
      shieldVisible = GetComponent<MeshRenderer>();
      shieldVisible.enabled=false;
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
            StartCoroutine(seeShield());
            other.transform.parent.gameObject.SendMessage("explodeSequence");
            shieldHealth -= 1;
            vessel.GetComponent<VesselMovement>().shieldRegenReset();
        }
        if(other.name.Contains("Projectile"))
        {
            if(!other.GetComponent<EnemyShooterProjectileCrash>()) return;
            StartCoroutine(seeShield());
            other.GetComponent<EnemyShooterProjectileCrash>().killProjectile();
            shieldHealth -= 5;
            vessel.GetComponent<VesselMovement>().shieldRegenReset();
        }
        lifebar_script.setShield(shieldHealth);
    }

    IEnumerator seeShield()
    {
        shieldVisible.enabled=true;
        yield return new WaitForSeconds(.5f);
        shieldVisible.enabled=false;

    }
}
