using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PetSaveGame : SaveGame
{
    public DateTime lastPlayDate;
    public float[] petStats;

    public PetSaveGame()
    {
        lastPlayDate = DateTime.Now;
        petStats = new float[] { 50, 50, 50, 50, 50 };
    }

}
