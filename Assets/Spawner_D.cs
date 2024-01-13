using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_D : MonoBehaviour
{
    public GameObject defenderPrefab;
    public int spawnAmount = 5;
    public int spawnAmountInitial = 5;
    public int spawnAmountContinuous = 5;
    public float xlimit = 5;
    public float ylimit = 5;
    public float zlimit = 1;
    public Animator anim;
    public bool keepSpawning = false;
    public bool alreadyEntered = false;
    public GameObject Camp;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie") && isDead == false)
        {
            spawnSwarm(spawnAmountInitial);
            keepSpawning = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            keepSpawning = false;
        }
    }

    private float timer;

    void Update()
    {
        if(keepSpawning)
        {
            timer += Time.deltaTime;
        }
        if(timer >= 2)
        {
            spawnSwarm(8);
            timer = 0;
        }
    }
    
    public Vector3 getRandomPosition()
    {
        var xpos = Random.Range((gameObject.transform.position.x - xlimit), (gameObject.transform.position.x + xlimit));
        var ypos = Random.Range((gameObject.transform.position.y - ylimit), (gameObject.transform.position.y + ylimit));
        var zpos = Random.Range((gameObject.transform.position.z - zlimit), (gameObject.transform.position.z + zlimit));

        return new Vector3(xpos, ypos, zpos);
    }

    void spawnSwarm(int spawnNumber)
    {
        for(var i = 0; i <= spawnNumber; i++)
        {
            var randomPosition = getRandomPosition();
            Instantiate(defenderPrefab, randomPosition, gameObject.transform.rotation);
        } 
    }

    private bool isDead = false;

    public void Explode()
    {
        anim.Play("Nest_Dead");
        if(Camp != null)
        {
            Camp.GetComponent<Animator>().Play("CampBeat");
        }
        keepSpawning = false;
        isDead = true;
    }
}
