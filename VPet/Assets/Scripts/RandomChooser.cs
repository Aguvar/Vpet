using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBag<T> {

    private List<T> distribution;

    public RandomBag()
    {
        distribution = new List<T>();
    }

    public void AddElement(T element, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            distribution.Add(element);
        }
    }

    public T ChooseRandom()
    {
        int randomIndex = Random.Range(0, distribution.Count);

        return distribution[randomIndex];
    }

}
