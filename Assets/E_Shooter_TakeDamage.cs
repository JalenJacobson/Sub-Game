using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Shooter_TakeDamage : MonoBehaviour
{
    public GameObject parentShooter;
    public float health;
    public Animator anim;
    public ShooterSlider healthbar;

    void Start()
    {
        health = 10f;
        anim = parentShooter.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Bullet"))
        {
            health -= 1;
            StartCoroutine(Dmg_Indicator());
            healthbar.setHealth(health);
            //should play take damage animation
        }
    }

    void Update()
    {
        if(health <= 0)
        {
            StartCoroutine(DeadShooter());
            //should play blow up animation
        }
    }

    public IEnumerator Dmg_Indicator()
    {
        gameObject.GetComponent<SkinnedMeshRenderer>().enabled=true;
        yield return new WaitForSeconds(.3f);
        gameObject.GetComponent<SkinnedMeshRenderer>().enabled=false;
        yield return new WaitForSeconds(.3f);
        gameObject.GetComponent<SkinnedMeshRenderer>().enabled=true;
        yield return new WaitForSeconds(.3f);
        gameObject.GetComponent<SkinnedMeshRenderer>().enabled=false;  
    }
    public IEnumerator DeadShooter()
    {
        parentShooter.SendMessage("StopShooting");
        anim.Play("DeadShooter");
        yield return new WaitForSeconds(3f);
        parentShooter.SendMessage("die");
        //Destroy(parentShooter);
        
    }
    public void lockOn()
        {
            parentShooter.SendMessage("lockOn");
        }
}
