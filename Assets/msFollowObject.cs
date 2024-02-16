using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msFollowObject : MonoBehaviour
{

    public GameObject MS;
    public float yOffset;
    public float zOffset;
    public bool shouldFollow = false;
    public GameObject ObjectivePannel;
    public bool Level2;

    // Start is called before the first frame update
    void Start()
    {
        if(Level2)
        {
            StartCoroutine("begin");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldFollow)
        {
            transform.position = MS.GetComponent<Transform>().position + new Vector3(0, yOffset, zOffset);  
        }
       
    }

    public void deathByFall()
    {
        StartCoroutine("fall");
    }

    public IEnumerator fall()
    {
        shouldFollow = false;
        yield return new WaitForSeconds(1.5f);
        shouldFollow = true;
    }
    public IEnumerator begin()
    {
        yield return new WaitForSeconds(2f);
        shouldFollow = true;
        ObjectivePannel.SetActive(true);
    }
}
