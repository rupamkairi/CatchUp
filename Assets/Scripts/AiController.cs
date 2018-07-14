using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour {

	public GameObject player;

	private NavMeshAgent agent;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	void FixedUpdate()
	{
        agent.destination = player.transform.position;
	}
}
