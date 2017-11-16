using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour {

    private float currentTimeInSeconds;
    private Transform sunTransform;
    private Light sun;

    //Range[0,1]
    private float timeOfTheDay;
    private float sunIntensity;

    private readonly float secondsInADay = 86400;

    // Use this for initialization
    void Start () {
        sunTransform = GetComponent<Transform>();
        currentTimeInSeconds = (float)System.DateTime.Now.TimeOfDay.TotalSeconds;
        sun = GetComponent<Light>();
        sunIntensity = sun.intensity;
	}
	
	// Update is called once per frame
	void Update () {
        currentTimeInSeconds = (float)System.DateTime.Now.TimeOfDay.TotalSeconds;

        timeOfTheDay = currentTimeInSeconds / secondsInADay;

        UpdateSun();
    }

    private void UpdateSun()
    {
        sunTransform.localRotation = Quaternion.Euler((timeOfTheDay * 360f) - 90, 170, 0);

        //float intensityMultiplier = 1;
        //if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        //{
        //    intensityMultiplier = 0;
        //}
        //else if (currentTimeOfDay <= 0.25f)
        //{
        //    intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        //}
        //else if (currentTimeOfDay >= 0.73f)
        //{
        //    intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        //}

        //sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
