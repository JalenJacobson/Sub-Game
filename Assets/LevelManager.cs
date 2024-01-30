using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown("m"))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void loadClimb()
    {
        SceneManager.LoadScene(1);
    }
    public void loadEnd()
    {
        SceneManager.LoadScene(2);
    }
}
