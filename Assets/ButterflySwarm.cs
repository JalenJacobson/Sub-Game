using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySwarm : MonoBehaviour
{
    public GameObject butterflyPrefab;
    public GameObject nestCirclePoint;
    public int spawnNumber;
    public List<GameObject> butterflys;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "egg")
        {
            for(var i = 0; i< spawnNumber; i++)
            {
                spawnButterfly();
            }
        }
    }

    public void spawnButterfly()
    {
        var butterfly = Instantiate(butterflyPrefab, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
        butterfly.GetComponent<Butterfly>().target = gameObject;
        butterflys.Add(butterfly);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("egg"))
        {
            foreach(GameObject butterfly in butterflys)
            {
                butterfly.GetComponent<Butterfly>().swarmEgg();
            }
        }
    }
}
