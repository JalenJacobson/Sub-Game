using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterProjectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 2000;
    public Rigidbody rb;
    public float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        target =  GameObject.FindGameObjectWithTag("Vessel");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 7);
        speed = 300;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed);

        Vector3 direction = target.GetComponent<Transform>().position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, timeCount);
        timeCount = timeCount + Time.deltaTime;
        // transform.rotation = toRotation;
    }
}
