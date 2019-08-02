using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
      public float timeRemaining = 2.0f;

    void Update () 
    {
        timeRemaining -= Time.deltaTime;
    }
    
    void OnGUI()
    {
        if(timeRemaining > 0)
        {
           
        }
        else
        {
            GUI.Label(new Rect(100, 100, 200, 100), "Time's Up");
        }
    }
}
