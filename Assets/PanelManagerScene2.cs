using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManagerScene2 : MonoBehaviour
{
    public GameObject RiddlePannel;
    public GameObject SolveRiddlePannel;
    public GameObject WinPannel;

   
    // Start is called before the first frame update
    void Start()
    {
        RiddlePannel.SetActive(false);
        SolveRiddlePannel.SetActive(false);
        WinPannel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            openPanel();
        }
        if(Input.GetKeyDown("o"))
        {
            closePanel();
        }
    }

    public void openPanel()
    {
        RiddlePannel.SetActive(true);
    }
    public void closePanel()
    {
        RiddlePannel.SetActive(false);
    }
}
