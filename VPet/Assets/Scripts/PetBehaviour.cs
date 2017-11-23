using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetBehaviour : MonoBehaviour
{

    public float initialHunger;
    public float initialThirst;
    public float initialFun;
    public float initialEnergy;
    public float initialHealth;

    //{Hunger, Thirst, Fun, Energy, Health}
    private float[] stats;

    //private bool moving = false;//Revise

    private Transform petTransform;
    private NavMeshAgent agent;

    public Transform corralBoundStart;
    public Transform corralBoundEnd;

    public float[] Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    private void Awake()
    {
        stats = new float[] { initialHunger, initialThirst, initialFun, initialEnergy, initialHealth };
    }

    // Use this for initialization
    void Start()
    {
        petTransform = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(HangAround());
    }

    // Update is called once per frame
    void Update()
    {
        Simulate(Time.deltaTime);
    }

    private IEnumerator HangAround()
    {
        while (true)
        {
            Vector3 destination = GenerateDestination();
            agent.SetDestination(destination);
            yield return new WaitForSeconds(8);//Think about how to make it move after it stopped moving 
        }
    }

    private Vector3 GenerateDestination()
    {
        float X = UnityEngine.Random.Range(corralBoundStart.position.x, corralBoundEnd.position.x);
        float Z = UnityEngine.Random.Range(corralBoundStart.position.z, corralBoundEnd.position.z);

        return new Vector3(X, petTransform.position.y, Z);
    }

    public void Simulate(float simTime)
    {
        stats[0] = stats[0] - simTime * 0.022f;
        stats[1] = stats[1] - simTime * 0.024f;
        stats[2] = stats[2] - simTime * 0.046f;
        stats[3] = stats[3] - simTime * 0.0115f;
        stats[4] = stats[4] + (stats[0] - 100) * 0.02f + (stats[1] - 200) * 0.02f + stats[3] * 0.02f;
        //Do something about magic numbers afterwards. Abstraction for different pets to have different needs.
    }





}
