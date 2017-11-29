using UnityEngine;

public class GUIDebug : MonoBehaviour {

    private double secondsSinceLastSession;

	// Use this for initialization
	void Start () {

        secondsSinceLastSession = SimManager.instance.secondsSinceLastSession;

	}

    private void OnGUI()
    {
        GUI.Label(new Rect(20,20,400,20), "Seconds since last session: " + Mathf.Floor((float)secondsSinceLastSession));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
