using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFilter : MonoBehaviour
{
    public float waterHeight;
 
    public bool isUnderwater;
    private Color normalColor;
    private Color underwaterColor;
 
    // Use this for initialization
    void Start () {
    normalColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
    underwaterColor = new Color (0.22f, 0.65f, 0.77f, 0.5f);
    SetUnderwater();
    }
 
 // Update is called once per frame
//  void Update () {
//  if ((transform.position.y < waterHeight) != isUnderwater) {
//  isUnderwater = transform.position.y < waterHeight;
//  if (isUnderwater) SetUnderwater ();
//  if (!isUnderwater) SetNormal ();
//  }
//  }
 
//  void SetNormal () {
//  RenderSettings.fogColor = normalColor;
//  RenderSettings.fogDensity = 0.01f;
 
//  }
 
 void SetUnderwater () {
 RenderSettings.fogColor = underwaterColor;
 RenderSettings.fogDensity = 1f;
 
 }
}
