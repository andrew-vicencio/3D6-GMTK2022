using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttack : MonoBehaviour
{

    public float damage;

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Enemy"){
            //deal dmg
        }
    }
}
