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
            if (timer < 1f)
            {   
                other.attachedRigidbody.AddForce(Vector3.forward * 5);
            }
            else
            {
                other.attachedRigidbody.AddForce(-Vector3.forward * 5);
                if (timer >= 2f) timer = 0f;
            }
        }
            
    }
}
