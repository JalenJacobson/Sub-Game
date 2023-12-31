using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class msFollowObject : MonoBehaviour
{

    public GameObject MS;
    public float yOffset;
    public bool shouldFollow = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldFollow)
        {
            transform.position = MS.GetComponent<Transform>().position + new Vector3(0, yOffset, 0);  
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
}
