using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public float damage = 1;

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Player"){
            Debug.Log("HIT");
            
            PlayerHealth enemy = collision.gameObject.GetComponent<PlayerHealth>();
            enemy?.damage(damage);
        }
    }
}
