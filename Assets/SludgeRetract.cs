using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeRetract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Bullet"))
        {
            //play animation to suck sludge back in
        }
        else if (collision.collider.name.Contains("weenie"))
        {
            
            collision.collider.SendMessage("takeDamage", 4);
            
            
        }
    }
}
