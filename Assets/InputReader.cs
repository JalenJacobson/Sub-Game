using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{

    public string inputGuess;
    public GameObject WinPannel;
    public GameObject GuessPannel;
    public GameObject Remediate;
    
    public void ReadStringInput(string inputReceive)
    {
        inputGuess = inputReceive;
        
    }

    public void submit()
    {
        if(inputGuess == "baby" || inputGuess == "Baby" || inputGuess == "BABY")
        {
            WinPannel.SetActive(true);
            GuessPannel.SetActive(false);
        }
        else Remediate.GetComponent<Animator>().Play("Remediate");
    }
}
