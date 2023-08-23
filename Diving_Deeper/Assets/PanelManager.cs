using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{

    public GameObject RiddlePannel;
    public bool riddlePannelIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
       RiddlePannel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RiddlePannelOpen()
    {
        RiddlePannel.SetActive(true);
        // Time.timeScale = 0f;
        riddlePannelIsOpen = true;
        // Music.volume = .24f;
        // EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }
    public void RiddlePannelClose()
    {
        RiddlePannel.SetActive(false);
        // Time.timeScale = 1f;
        riddlePannelIsOpen = false;
        // Music.volume = .24f;
        // EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }
}
