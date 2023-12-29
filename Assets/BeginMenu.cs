using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginMenu : MonoBehaviour
{
    public GameObject ResumerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Begin()
    {
        Time.timeScale = 1f;
        ResumerCanvas.SetActive(false);
    }
}
