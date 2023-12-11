using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_DTraining : MonoBehaviour
{
    public GameObject defenderPrefab;
    public int spawnAmount = 5;
    public float xlimit = 5;
    public float ylimit = 5;
    public float zlimit = 1;
    public int enemiesKilled = 5;
    public SchoolLessons SchoolUI;

    void Update()
    {
        if(enemiesKilled <= 0)
        {
            SchoolUI.SendMessage("P2Lesson2");
            Destroy(gameObject);
        }
    }


    public void OnTriggerStay(Collider other)
    {
        print(other.name);
        if(other.name.Contains("weenie"))
        {
            StartCoroutine("spawn");
        }
        
    }

    public Vector3 getRandomPosition()
    {
        var xpos = Random.Range((gameObject.transform.position.x - xlimit), (gameObject.transform.position.x + xlimit));
        var ypos = Random.Range((gameObject.transform.position.y - ylimit), (gameObject.transform.position.y + ylimit));
        var zpos = Random.Range((gameObject.transform.position.z - zlimit), (gameObject.transform.position.z + zlimit));

        return new Vector3(xpos, ypos, zpos);
    }

    public IEnumerator spawn()
    {
        yield return new WaitForSeconds(1f);
        for(var i = 0; i <= spawnAmount; i++)
            {
                print(i);
                var randomPosition = getRandomPosition();
                Instantiate(defenderPrefab, randomPosition, gameObject.transform.rotation);
            } 
    }

    public void killedOne()
    {
        enemiesKilled -= 1;
    }
}
