using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTraining : MonoBehaviour
{
    public SwimSchool schoolManager;
    public bool schoolSkipped = false;
    public GameObject Lesson21;
    public GameObject Lesson22;
    public Lesson2Hit1 Hit1;
    public Lesson2Hit2 Hit2;
    public VesselMovement Weenie;
    // Start is called before the first frame update
    void Start()
    {
        Hit1 = Lesson21.GetComponent<Lesson2Hit1>();
        Hit2 = Lesson22.GetComponent<Lesson2Hit2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Bullet") && schoolSkipped == false)
        {
            schoolSkipped = true;
            StartCoroutine("getFirstRiddle");
            Weenie.SendMessage("startGame");
        }
    }

    public IEnumerator getFirstRiddle()
    {
        Hit1.moveAtVessel();
        yield return new WaitForSeconds(.2f);
        Hit2.moveAtVessel();
        yield return new WaitForSeconds(2f);
        schoolManager.SendMessage("schoolisSkipped");
    }

    public void NotSkipped()
    {
        Destroy(gameObject);
    }
}
