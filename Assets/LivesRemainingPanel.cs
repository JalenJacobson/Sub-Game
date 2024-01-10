using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesRemainingPanel : MonoBehaviour
{
    public int livesRemaining;
    public GameObject LoseScreen;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        livesRemaining = 3;
        LoseScreen = GameObject.Find("LoseScreen");
        LoseScreen.SetActive(false);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(livesRemaining == 2)
        {
            anim.Play("2Lives");
        }
        else if(livesRemaining == 1)
        {
            anim.Play("1Life");
        }
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
