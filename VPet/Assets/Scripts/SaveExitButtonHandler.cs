using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveExitButtonHandler : MonoBehaviour {

    private Button saveExitButton;

	// Use this for initialization
	void Start () {
        saveExitButton = GetComponent<Button>();
        saveExitButton.onClick.AddListener(SaveAndExit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SaveAndExit()
    {
        Application.Quit();
    }
    
}
