using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public float damage = 1;

    void OnCollisionEnter(Collision collision){
        if(collision.collider.gameObject.tag == "Player"){
            Debug.Log("HIT PLAYER");
            
            PlayerHealth player = collision.collider.gameObject.GetComponent<PlayerHealth>();
            player?.damage(damage);
        }
    }
}
