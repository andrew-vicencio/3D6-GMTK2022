using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    private Transform player;
    private GameManager gameManager;

    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private float acceleration = 5f;

    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float prefDist = 0f;

    protected void Awake() {
        player = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.running && player != null) {
            transform.LookAt(player);

            Vector3 target = offset(player);
            transform.position = Vector3.MoveTowards(transform.position, player.position, maxSpeed * Time.deltaTime);
            //transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 1f / acceleration, maxSpeed);
        }
        else if(player == null && gameManager.running){
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    private Vector3 offset(Transform target){
        Vector3 distanceVector = target.position - transform.position;
        Vector3 distanceVectorNormalized = distanceVector.normalized;
        float magnitude = Vector3.Distance(target.position, transform.position);
        Vector3 targetPosition = (distanceVectorNormalized * (magnitude - prefDist));
        return targetPosition;
    }
}
