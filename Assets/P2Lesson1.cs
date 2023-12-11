using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Lesson1 : MonoBehaviour
{
    public int enemiesKilled = 5;
    public SchoolLessons SchoolUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesKilled <= 0)
        {
            SchoolUI.SendMessage("P2Lesson2");
            Destroy(gameObject);
        }
    }

    public void killedOne()
    {
        enemiesKilled -= 1;
    }
}
