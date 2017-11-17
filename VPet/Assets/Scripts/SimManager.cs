using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SimManager : MonoBehaviour
{

    private DateTime lastPlayDate;
    public double secondsSinceLastSession;

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
}
