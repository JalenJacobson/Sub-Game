using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Pointer : MonoBehaviour
{
    private Vector3 target;
    private RectTransform pointerRectTransform;
    public GameObject pointer;
    // Start is called before the first frame update
    void Awake()
    {
        target = new Vector3(0, 0);
        pointerRectTransform = pointer.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPosition = target;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
    }
    public void nextTrigger(GameObject newTrigger)
    {
        //target = newTrigger.GetComponent<Transform>();
    }
}
