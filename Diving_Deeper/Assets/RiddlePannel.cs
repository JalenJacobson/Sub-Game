using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiddlePannel : MonoBehaviour
{

    public List<GameObject> riddleParts;
    public int riddlePartsUnlocked = -1;
    // Start is called before the first frame update

    void Awake()
    {
        foreach(GameObject part in riddleParts)
        {
            part.SetActive(false);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addRiddlePart()
    {
        riddlePartsUnlocked++;
        for(var i = 0; i <= riddlePartsUnlocked; i++)
        {
            riddleParts[i].SetActive(true);
        }
    }
}
