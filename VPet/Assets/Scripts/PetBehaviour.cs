using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetBehaviour : MonoBehaviour {

    private float hunger;
    private float thirst;
    private float fun;
    private float energy;
    private float health;
    private bool moving = false;//Revise

    private Transform petTransform;
    private NavMeshAgent agent;

    public Transform corralBoundStart;
    public Transform corralBoundEnd;

    // Use this for initialization
    void Start () {
        petTransform = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(HangAround());
	}
	
	// Update is called once per frame
	void Update () {
		
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
        float X = UnityEngine.Random.Range(corralBoundStart.position.x,corralBoundEnd.position.x);
        float Z = UnityEngine.Random.Range(corralBoundStart.position.z,corralBoundEnd.position.z);

        return new Vector3(X, petTransform.position.y, Z);
    }
}
