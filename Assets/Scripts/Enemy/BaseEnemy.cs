using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    [SerializeField] private Vector3 target;

    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private float maxAcceleration = 1f;
    [SerializeField] private float prefDist = 5f;

    protected void Awake() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        agent.acceleration = maxAcceleration;
        agent.speed = maxSpeed;
        agent.autoRepath = true;
        agent.stoppingDistance = prefDist;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.position);
        agent.SetDestination(player.position);
    }
}
