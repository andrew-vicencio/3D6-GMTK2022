using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float speed = 20;
    Vector3 directon;

    void Update() 
    {
        transform.Translate (Vector3.forward * speed * Time.deltaTime);
        //var x = transform.position.x + speed * Time.deltaTime;
        //var z = transform.position.z + speed * Time.deltaTime;
        //transform.position = new Vector3(x,transform.position.y,0);
    }
    
}
