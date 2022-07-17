using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    private Transform player;

    public float windup = 1f;
    public float DEFAULT_COOLDOWN = 5f;
    public float cooldown = 0f;
    public float attackRange = 4f;

    public GameObject bullet;
    public Transform bulletSpot;
    public ParticleSystem fire;

    private void Awake() {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        float dist = Vector3.Distance(player.position, transform.position);
        if (cooldown <= 0 && dist < attackRange) {
            fire.Play();
            Quaternion rot = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
            Instantiate(bullet, bulletSpot.position,rot);
            cooldown = DEFAULT_COOLDOWN;
        } else {
            cooldown -= Time.deltaTime;
        }
    }
}
