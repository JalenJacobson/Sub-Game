using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_D : MonoBehaviour
{
    public GameObject defenderPrefab;
    public int spawnAmount = 5;
    public float xlimit = 5;
    public float ylimit = 5;
    public float zlimit = 1;


    public void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if(other.name.Contains("weenie"))
        {
            print("should work");
           for(var i = 0; i <= spawnAmount; i++)
            {
                print(i);
                var randomPosition = getRandomPosition();
                Instantiate(defenderPrefab, randomPosition, gameObject.transform.rotation);
            } 
        }
        
    }

    public Vector3 getRandomPosition()
    {
        var xpos = Random.Range((gameObject.transform.position.x - xlimit), (gameObject.transform.position.x + xlimit));
        var ypos = Random.Range((gameObject.transform.position.y - ylimit), (gameObject.transform.position.y + ylimit));
        var zpos = Random.Range((gameObject.transform.position.z - zlimit), (gameObject.transform.position.z + zlimit));

        return new Vector3(xpos, ypos, zpos);
    }
}
