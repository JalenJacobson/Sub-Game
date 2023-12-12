using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollaShooterMissed : MonoBehaviour
{
    public int misses = 3;
    public SchoolLessons SchoolUI;
    public GameObject ShooterHolla;
    public GameObject Spawner_Training;
    public bool P1Completed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(misses <= 0 && P1Completed == false)
        {
            StartCoroutine("trainingP1Completed");
            Spawner_Training.SetActive(true);
            ShooterHolla.SetActive(false);
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("projectile"))
        {
            misses -= 1;
        }
    }
    public IEnumerator trainingP1Completed()
    {
        P1Completed = true;
        SchoolUI.SendMessage("P1Complete");
        yield return new WaitForSeconds(5f);
        SchoolUI.SendMessage("P2Lesson1");
    }
}
