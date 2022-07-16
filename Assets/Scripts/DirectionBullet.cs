using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField] public float damage = 20;
    public float speed = 20;
    Vector3 directon;

    void Update() 
    {
        transform.Translate (Vector3.forward * speed * Time.deltaTime);
        //var x = transform.position.x + speed * Time.deltaTime;
        //var z = transform.position.z + speed * Time.deltaTime;
        //transform.position = new Vector3(x,transform.position.y,0);
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("HIT!");
        if(collision.gameObject.tag == "Enemy"){
            Destroy(gameObject);
            //play particle effect
            //damage enemy
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy?.damage(damage);
        }
    }
    
}
