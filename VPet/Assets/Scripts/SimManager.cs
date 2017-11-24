using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using Assets.Scripts;

public class SimManager : MonoBehaviour
{
    [HideInInspector]
    public double secondsSinceLastSession;
    
    private DateTime lastPlayDate;
    private PetBehaviour pet;
    private float[] loadedStats;
    private bool loaded = false;

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
