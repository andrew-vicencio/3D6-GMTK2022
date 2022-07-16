using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanKnives : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] public float damage = 20;
    public float speed = 20;
    public float travelTime = 30;

    void Start()
    {
    }

    void Update() 
    {
        if(travelTime > 0){
            transform.Translate (Vector3.forward * speed * Time.deltaTime);
            travelTime -= Time.deltaTime;
        }
        else{

            Destroy(gameObject);

        }
        
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
