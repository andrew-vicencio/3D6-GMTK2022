using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    // Start is called before the first frame update
    

    public float speed = 20;
    Vector3 directon;
    public float travelTime = 30;
    public float explosionTime = 10;
    Animator anim;
    public GameObject explosion;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update() 
    {
        if(travelTime > 0){
            transform.Translate (Vector3.forward * speed * Time.deltaTime);
            travelTime -= Time.deltaTime;
        }
        else{

            if(explosionTime > 0){
                explosionTime -= Time.deltaTime;
                anim.enabled = true;
            }
            else{
                Debug.Log("Explode!");
                Instantiate(explosion,transform.position,Quaternion.identity);
                Destroy(gameObject);
            }

        }
        
        //var x = transform.position.x + speed * Time.deltaTime;
        //var z = transform.position.z + speed * Time.deltaTime;
        //transform.position = new Vector3(x,transform.position.y,0);
    }
    
}
