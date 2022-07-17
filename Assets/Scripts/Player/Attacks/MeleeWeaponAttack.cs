using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttack : MonoBehaviour
{

    public float damage = 50;
    public GameObject damageEffect;
    AudioSource audioSource;
    public AudioClip hurtAudio;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    void PlaySound(AudioClip whichSound)
    {
        Debug.Log(audioSource);
        audioSource.PlayOneShot(whichSound);
        Debug.Log("Sound was played:" + whichSound);
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Enemy"){
            PlaySound(hurtAudio);
            Debug.Log("HIT");
            Instantiate(damageEffect,collision.transform.position,Quaternion.identity);
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy?.damage(damage);
        }
    }
}
