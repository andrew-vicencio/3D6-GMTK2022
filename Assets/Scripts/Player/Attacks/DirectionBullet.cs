using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionBullet : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [SerializeField] private float damage = 20;
    public float speed = 20;
    Vector3 directon;

    void Update() 
    {
        transform.Translate (Vector3.forward * speed * Time.deltaTime);
        //var x = transform.position.x + speed * Time.deltaTime;
        //var z = transform.position.z + speed * Time.deltaTime;
        //transform.position = new Vector3(x,transform.position.y,0);
    }

    void PlaySound(AudioClip whichSound)
    {
        Debug.Log(audioSource);
        audioSource.PlayOneShot(whichSound);
        Debug.Log("Sound was played:" + whichSound);
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("HIT!");
        if(collision.gameObject.tag == "Enemy"){
            Destroy(gameObject);
            PlaySound(hurtSound);
            //play particle effect
            //damage enemy
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy?.damage(damage);
        }
    }
    
}
