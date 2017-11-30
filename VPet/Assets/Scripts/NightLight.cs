using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class NightLight : MonoBehaviour {

    public float baseIntensity;
    public float flickerRange;

    private Light lightSource;

	// Use this for initialization
	void Start () {
        lightSource = GetComponent<Light>();
        lightSource.intensity = baseIntensity;

        StartCoroutine(Flicker());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Flicker()
    {
        float intensityChange = Random.Range(-flickerRange, flickerRange);

        lightSource.intensity = baseIntensity + intensityChange;

        yield return new WaitForSeconds(0.2f);
    }
}
