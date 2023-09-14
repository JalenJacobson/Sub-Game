using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openURL : MonoBehaviour
{
    public void openVideo()
    {
        StartCoroutine("finishGame");
    }

    public IEnumerator finishGame()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=_5OvgQW6FG4");
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
