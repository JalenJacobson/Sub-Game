using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimSchool : MonoBehaviour
{
    public bool swimSchoolCompleted = false;
    public bool schoolStarted = false;
    public OpenDoor Door;
    public GameObject shooterTraining;
    public SchoolLessons SchoolUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Bullet") && schoolStarted == false)
        {
            SchoolUI.SendMessage("P1Lesson1");
            schoolStarted = true;
            //swimSchoolCompleted = true;
        }
    }

    public void schoolisDone()
    {
        swimSchoolCompleted = true;
        StartCoroutine("schoolsOut");
    }

    public IEnumerator schoolsOut()
    {
        yield return new WaitForSeconds(3f);
        Door.SendMessage("beginJourney");
        
    }
    public void destroySchool()
    {
        Destroy(gameObject);
    }

}
