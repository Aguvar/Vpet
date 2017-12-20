using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public GameObject objectTooltip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        LoadInventory();
    }

    private void LoadInventory()
    {
        throw new NotImplementedException();
        //Play open inventory animation
    }

    private void OnDisable()
    {
        //Play close inventory animation
    }
}
