using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttack : MonoBehaviour
{

    public float damage = 50;
    public GameObject damageEffect;

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Enemy"){
            Debug.Log("HIT");
            Instantiate(damageEffect,collision.transform.position,Quaternion.identity);
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy?.damage(damage);
        }
    }
}
