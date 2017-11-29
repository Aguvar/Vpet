using System;
using UnityEngine;

public class DayNightController : MonoBehaviour {

    private float currentTimeInSeconds;
    private Transform sunTransform;
    private Light sun;

    //Range[0,1]
    private float timeOfTheDay;
    private float sunInitialIntensity;

    private readonly float secondsInADay = 86400;

    // Use this for initialization
    void Start () {
        sunTransform = GetComponent<Transform>();
        currentTimeInSeconds = (float)DateTime.Now.TimeOfDay.TotalSeconds;
        sun = GetComponent<Light>();
        sunInitialIntensity = sun.intensity;
	}
	
	// Update is called once per frame
	void Update () {
        currentTimeInSeconds = (float)DateTime.Now.TimeOfDay.TotalSeconds;

        timeOfTheDay = currentTimeInSeconds / secondsInADay;

        UpdateSun();
    }

    private void UpdateSun()
    {
        sunTransform.localRotation = Quaternion.Euler((timeOfTheDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;
        if (timeOfTheDay <= 0.23f || timeOfTheDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (timeOfTheDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((timeOfTheDay - 0.23f) * (1 / 0.02f));
        }
        else if (timeOfTheDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((timeOfTheDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
