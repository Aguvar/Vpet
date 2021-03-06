﻿using System;
using UnityEngine;
using Assets.Scripts;
using System.Collections.Generic;

public class SimManager : MonoBehaviour
{
    [HideInInspector]
    public double secondsSinceLastSession;
    
    private DateTime lastPlayDate;
    private PetBehaviour pet;
    private float[] loadedStats;
    private bool loaded = false;

    private List<InventoryItem> inventory; 

    public static SimManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            instance = null;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        Load();
    }

    // Use this for initialization
    void Start()
    {
        pet = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetBehaviour>();

        inventory = new List<InventoryItem>();

        if (loaded)
        {
            pet.Stats = loadedStats;//What happens if there is no save data? 
        }
        pet.Simulate((float)secondsSinceLastSession);

    }

    private void OnEnable()
    {
        
    }

    private void Load()
    {
        PetSaveGame save;
        try
        {
            save = (PetSaveGame)SaveGameSystem.LoadGame("VPetSave");
            loaded = true;
        }
        catch (NonExistingSaveException e)
        {
            save = new PetSaveGame();
        }

        loadedStats = save.petStats;
        secondsSinceLastSession = (DateTime.Now - save.lastPlayDate).TotalSeconds;
    }

    private void OnDisable()
    {
        Save();
    }

    private void Save()
    {
        PetSaveGame save = new PetSaveGame();
        save.lastPlayDate = DateTime.Now;
        save.petStats = pet.Stats;

        SaveGameSystem.SaveGame(save, "VPetSave");
    }

    void Update()
    {

    }
}
