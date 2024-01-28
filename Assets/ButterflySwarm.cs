using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySwarm : MonoBehaviour
{
    public GameObject butterflyPrefab;
    public GameObject nestCirclePoint;
    public int spawnNumber;
    public List<GameObject> butterflys;

    public void Start()
    {
        spawnNumber = 3;

        for(var i = 0; i< spawnNumber; i++)
        {
            spawnButterfly();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("egg"))
        {
            foreach(GameObject butterfly in butterflys)
            {
                butterfly.GetComponent<Butterfly>().swarmEgg();
            }
        }
    }

    public void spawnButterfly()
    {
        var butterfly = Instantiate(butterflyPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        butterfly.GetComponent<Butterfly>().target = gameObject;
        butterflys.Add(butterfly);
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }
}
