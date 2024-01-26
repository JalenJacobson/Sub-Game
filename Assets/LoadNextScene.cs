using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
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
        if(other.tag == "egg")
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(2);
    }

    public void restartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
