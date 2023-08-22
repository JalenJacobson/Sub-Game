using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishyWashy : MonoBehaviour
{
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (timer < 3f)
            {   
                other.attachedRigidbody.AddForce(Vector3.forward * 50);
            }
            else
            {
                other.attachedRigidbody.AddForce(-Vector3.forward * 50);
                if (timer >= 6f) timer = 0f;
            }
        }
            
    }
}
