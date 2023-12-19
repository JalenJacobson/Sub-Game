using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_D : MonoBehaviour
{
    public GameObject defenderPrefab;
    public int spawnAmountInitial = 5;
    public int spawnAmountContinuous = 5;
    public float xlimit = 5;
    public float ylimit = 5;
    public float zlimit = 1;
    public Animator anim;
    public bool keepSpawning = false;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("weenie"))
        {
            spawnSwarm(spawnAmountInitial);
            keepSpawning = true;
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

    void spawnSwarm(int spawnNumber)
    {
        for(var i = 0; i <= spawnNumber; i++)
        {
            var randomPosition = getRandomPosition();
            Instantiate(defenderPrefab, randomPosition, gameObject.transform.rotation);
        } 
    }

    public Vector3 getRandomPosition()
    {
        var xpos = Random.Range((gameObject.transform.position.x - xlimit), (gameObject.transform.position.x + xlimit));
        var ypos = Random.Range((gameObject.transform.position.y - ylimit), (gameObject.transform.position.y + ylimit));
        var zpos = Random.Range((gameObject.transform.position.z - zlimit), (gameObject.transform.position.z + zlimit));

        return new Vector3(xpos, ypos, zpos);
    }

    public void Explode()
    {
        keepSpawning = false;
        anim.Play("Nest_Dead");
    }
}
