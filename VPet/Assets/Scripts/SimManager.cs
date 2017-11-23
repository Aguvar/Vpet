using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

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
        if (File.Exists(Application.persistentDataPath + "/sessionData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/sessionData.dat", FileMode.Open);
            SessionData data = (SessionData)bf.Deserialize(file);
            file.Close();

            lastPlayDate = data.lastPlayDate;
            loadedStats = data.petStats;

            loaded = true;
        }
        else
        {
            File.Create(Application.persistentDataPath + "/sessionData.dat");
            lastPlayDate = DateTime.Now;
        }

        secondsSinceLastSession = (DateTime.Now - lastPlayDate).TotalSeconds;
    }

    private void OnDisable()
    {
        Save();
    }

    private void Save()
    {
        lastPlayDate = DateTime.Now;
        SessionData data = new SessionData();
        data.lastPlayDate = lastPlayDate;
        data.petStats = pet.Stats;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/sessionData.dat", FileMode.Open);

        bf.Serialize(file, data);
        file.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[Serializable]
class SessionData
{
    public DateTime lastPlayDate;
    public float[] petStats;
}
