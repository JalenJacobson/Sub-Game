using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanel : MonoBehaviour
{
    public Button Climb;
    public Button End;
    public bool climbUnlocked;
    public bool endUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetInt("climb", 0);
        // PlayerPrefs.SetInt("end", 0);

        if(PlayerPrefs.GetInt("climb") == 0) climbUnlocked = false; 
        else climbUnlocked = true;
        if(PlayerPrefs.GetInt("end") == 0) endUnlocked = false; 
        else endUnlocked = true;
        
        Climb.interactable = climbUnlocked;
        End.interactable = endUnlocked;
        // End.interactable = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
