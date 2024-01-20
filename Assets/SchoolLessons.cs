using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolLessons : MonoBehaviour
{
    public GameObject ResumerCanvas, StartSchoolCanvas;
    public GameObject P1Lesson1_ui, P1Lesson2_ui, P1Lesson3_ui, P1Completed_ui;
    public GameObject P2Lesson1_ui, P2Lesson2_ui, P2Completed_ui;
    // Start is called before the first frame update
    void Start()
    {
        ResumerCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void P1Lesson1()
    {
        StartSchoolCanvas.SetActive(false);
       // ResumerCanvas.SetActive(true);
        P1Lesson1_ui.SetActive(true);
        //Time.timeScale = 0f;
    }
    public void P1Lesson2()
    {
        //ResumerCanvas.SetActive(true);
        P1Lesson1_ui.SetActive(false);
        P1Lesson2_ui.SetActive(true);
        //Time.timeScale = 0f;
    }
    public void P1Lesson3()
    {
        //ResumerCanvas.SetActive(true);
        P1Lesson2_ui.SetActive(false);
        P1Lesson3_ui.SetActive(true);
        //Time.timeScale = 0f;
    }
    public void P1Complete()
    {
       // ResumerCanvas.SetActive(true);
        P1Lesson3_ui.SetActive(false);
        P1Completed_ui.SetActive(true);
        //Time.timeScale = 0f;
    }



    public void P2Lesson1()
    {
       // ResumerCanvas.SetActive(true);
        P1Completed_ui.SetActive(false);
        P2Lesson1_ui.SetActive(true);
        //Time.timeScale = 0f;
    }
    public void P2Lesson2()
    {
        //ResumerCanvas.SetActive(true);
        P2Lesson1_ui.SetActive(false);
        P2Lesson2_ui.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void P2Lesson3()
    {
        //ResumerCanvas.SetActive(true);
        P2Lesson2_ui.SetActive(false);
        P2Completed_ui.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void Begin()
    {
        P1Lesson1_ui.SetActive(false);
        P1Lesson2_ui.SetActive(false);
        P1Lesson3_ui.SetActive(false);
        P1Completed_ui.SetActive(false);
        P2Lesson1_ui.SetActive(false);
        P2Lesson2_ui.SetActive(false);
        P2Completed_ui.SetActive(false);
        ResumerCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
