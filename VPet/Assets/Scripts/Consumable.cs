using UnityEngine;
using System.Collections;

public class Consumable : ItemObject
{

    public float[] Effect;
    public int DecayTime;

    public Consumable()
    {
        Effect = new float[] { 0, 0, 0, 0, 0 };
    }
}
