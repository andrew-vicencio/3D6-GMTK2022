using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : MonoBehaviour
{
    // Start is called before the first frame update
    

    public float speed = 20;
    public float travelTime = 30;
    public float rotationSpeed = 5;

    GameObject target;

    void Awake()
    {
        float distance = 9999;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")){
            float tempDist = Vector3.Distance(obj.transform.position,transform.position);
            if(tempDist < distance){
                distance = tempDist;
                target = obj;
            }
        }
        if(target == null){
            Destroy(gameObject);
        }
        speed = speed+Random.Range(0.0f,2.0f);
        rotationSpeed = rotationSpeed+Random.Range(-3.0f,3.0f);
    }

    void Update() 
    {
        if(travelTime > 0){
            Vector3 moveTO = new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position,moveTO, speed * Time.deltaTime);
            transform.Translate (Vector3.forward * speed * Time.deltaTime);
            travelTime -= Time.deltaTime;
            Vector3 direction = moveTO - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime *rotationSpeed);
            transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
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
        }
    }
    
}
