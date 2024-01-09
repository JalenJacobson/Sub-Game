using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesRemainingPanel : MonoBehaviour
{
    public int livesRemaining;
    public GameObject LoseScreen;
    // Start is called before the first frame update
    void Start()
    {
        livesRemaining = 3;
        LoseScreen = GameObject.Find("LoseScreen");
        LoseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loseALife()
    {
        
        livesRemaining --;
        if(livesRemaining == 0)
        {
            StartCoroutine("loseScreenSequence");
        }
    }

    public IEnumerator loseScreenSequence()
    {
        yield return new WaitForSeconds(1.5f);
        LoseScreen.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(1);
    }
}
