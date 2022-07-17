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
    private Vector3 storedVelocity = Vector3.zero;

    [SerializeField] private float minDist = 0f;
    [SerializeField] private float maxSpeed = 1f;

    [SerializeField] private Vector3 steer = Vector3.zero;
    [SerializeField] private float SEEK_FORCE = 3f;
    [SerializeField] private float FLEE_FORCE = 1f;

    protected void Awake() {
        player = GameObject.Find("Player").transform;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    void Start() {
        if(player == null && gameManager.running){
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update() {
        if(player == null && gameManager.running){
            player = GameObject.FindWithTag("Player").transform;
        }

        if (gameManager.running && player != null) {
            float steerForce = SEEK_FORCE;

            Vector3 desired = (player.position - transform.position).normalized * maxSpeed;
            desired.y = 0;

            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= minDist) {
                desired = desired * -1;
                steerForce = FLEE_FORCE;
            }

            steer = (desired - rb.velocity).normalized * steerForce;
        }

        if (storedVelocity.magnitude > 0) {
            rb.velocity = storedVelocity;
            storedVelocity = Vector3.zero;
        }

        if (!gameManager.running) {
            storedVelocity = rb.velocity;
            rb.velocity = Vector3.zero;
        }
    }

    void LateUpdate() {
        rb.velocity += steer * Time.deltaTime;
        if (rb.velocity.magnitude < maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        steer = Vector3.zero;
    }

}
