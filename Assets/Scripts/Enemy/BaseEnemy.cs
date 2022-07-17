using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    private Transform player;
    private GameManager gameManager;

    [SerializeField] Vector3 target;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private float acceleration = 2f;

    [SerializeField] private float minDist = 1;
    [SerializeField] private float maxSpeed = 2f;

    [SerializeField] private Vector3 steer = Vector3.zero;

    protected void Awake() {
        player = GameObject.Find("Player").transform;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
    // {
    //     if (gameManager.running) {
    //         transform.LookAt(player);

    //         if (Vector3.Distance(player.position, transform.position) >= minDist) {
    //             transform.position = Vector3.MoveTowards(transform.position, player.position, maxSpeed * Time.deltaTime);
    //         } else if (Vector3.Distance(player.position, transform.position) < minDist) {
    //             transform.position = Vector3.MoveTowards(transform.position, transform.forward * -1, maxSpeed * Time.deltaTime);
    //         }
    //         // transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 1f / acceleration, maxSpeed);
    //     }
        if (gameManager.running && player != null) {
            Vector3 desired = (player.position - transform.position).normalized * maxSpeed;
            desired.y = 0;
            steer = (desired - rb.velocity).normalized * maxSpeed;
        }
        else if(player == null && gameManager.running){
            player = GameObject.FindWithTag("Player").transform;
        }

    private void LateUpdate() {
        rb.velocity += steer * Time.deltaTime;
        if (rb.velocity.magnitude < maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        steer = Vector3.zero;
    }
}
}
