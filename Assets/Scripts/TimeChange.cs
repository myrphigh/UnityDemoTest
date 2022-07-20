using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    public float slowdownFactor = 0.1f;
    void Start()
    {
        
    }

    public void slowMotion()
    {
    	Time.timeScale = slowdownFactor;
    	Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void stopSlowMotion()
    {
    	Time.timeScale = 1f;
    	Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
