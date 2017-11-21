﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    public PetBehaviour pet;
    public Slider[] statSliders;
    public float sliderDampening;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSliders();
	}

    private void UpdateSliders()
    {
        float[] statsArray = pet.Stats;

        for (int stat = 0; stat < statSliders.Length; stat++)
        {
            statSliders[stat].value = Mathf.MoveTowards(statSliders[stat].value, statsArray[stat], sliderDampening);
        }
        
    }
}
