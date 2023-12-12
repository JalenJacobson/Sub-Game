using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolLessons : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void P1Lesson1()
    {
        anim.Play("P1StartSchool");
    }
    public void P1Lesson2()
    {
        anim.Play("P1Lesson2");
    }
    public void P1Lesson3()
    {
        anim.Play("P1Lesson3");
    }
    public void P1Complete()
    {
        anim.Play("P1Completed");
    }



    public void P2Lesson1()
    {
        anim.Play("P2StartSchool");
    }
    public void P2Lesson2()
    {
        anim.Play("P2Lesson2");
    }
    public void P2Lesson3()
    {
        anim.Play("P1StartSchool");
    }
}
