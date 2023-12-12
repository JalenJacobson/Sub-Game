using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_DTraining : MonoBehaviour
{
    public GameObject defenderPrefab;
    public int spawnAmount = 1;
    public int spawn2Amount = 20;
    public float xlimit = 5;
    public float ylimit = 5;
    public float zlimit = 1;
    public int enemiesKilled = 0;
    public SchoolLessons SchoolUI;
    public bool spawned = false;
    public bool lastSpawn = false;

    void Update()
    {
        if(enemiesKilled == 5 && spawned == true)
        {
            SchoolUI.SendMessage("P2Lesson2");
            //spawn2();
        }
        if(enemiesKilled >= 25 && lastSpawn == false)
        {
            SchoolUI.SendMessage("P2Lesson3");
            Destroy(gameObject, 1);
        }
    }


    public void OnTriggerStay(Collider other)
    {
        print(other.name);
        if(other.name.Contains("weenie") && spawned == false && enemiesKilled <= 5)
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
        spawned = true;
        yield return new WaitForSeconds(5f);
        for(var i = 0; i <= spawnAmount; i++)
            {
                print(i);
                var randomPosition = getRandomPosition();
                Instantiate(defenderPrefab, randomPosition, gameObject.transform.rotation);
            } 
        spawned = false;
        // if(enemiesKilled <= 5)
        // {
        //     spawned = false;
        // }
        // else if(enemiesKilled >= 6)
        // {
        //     spawned = true;
        // }
    }

    public void spawn2()
    {
        lastSpawn = true;
        for(var i = 0; i <= spawn2Amount; i++)
            {
                print(i);
                var randomPosition = getRandomPosition();
                Instantiate(defenderPrefab, randomPosition, gameObject.transform.rotation);
            } 
    }

    public void killedOne()
    {
        enemiesKilled += 1;
    }
}
