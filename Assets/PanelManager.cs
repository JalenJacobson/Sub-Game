using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{

    public GameObject RiddlePannel;
    public GameObject NewRiddleAvalable;
    public GameObject panelOpenButton;
    public GameObject RiddleGuessInput;
    public GameObject RiddleGuessPannel;
    public GameObject WinPannel;
    public GameObject SettingsPanel;
    public GameObject LevelsPanel;
    public GameObject StartPanel;
    
    public string inputGuess;
    public bool riddlePannelIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        NewRiddleAvalable = GameObject.Find("NewIndicator");
        panelOpenButton = GameObject.Find("RiddleButton");
        //RiddleGuessInput = GameObject.Find("RiddleGuess");
        // RiddlePannel.SetActive(false); 
        // panelOpenButton.SetActive(false);
        // NewRiddleAvalable.SetActive(false); 
        // RiddleGuessPannel.SetActive(false); 
        // WinPannel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey("escape"))
        {
            closeApplication();
        }  
    }

    public void closeApplication()
    {
        Application.Quit();
    }

    public void RiddlePannelOpen()
    {
        RiddlePannel.SetActive(true);
        NewRiddleAvalable.SetActive(false);
        panelOpenButton.SetActive(false);
        // Time.timeScale = 0f;
        riddlePannelIsOpen = true;
        // Music.volume = .24f;
        // EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }
    public void RiddlePannelClose()
    {
        RiddlePannel.SetActive(false);
        panelOpenButton.SetActive(true);
        // Time.timeScale = 1f;
        riddlePannelIsOpen = false;
        // Music.volume = .24f;
        // EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
    }
    public void SettingsPannelOpen()
    {
        StartPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    public void SettingsPannelClose()
    {
        StartPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
    
    public void LevelsPannelOpen()
    {
        StartPanel.SetActive(false);
        LevelsPanel.SetActive(true);
    }
    public void LevelsPannelClose()
    {
        StartPanel.SetActive(true);
        LevelsPanel.SetActive(false);
    }
    

    
}
