using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float speed = 20;

    void Update() 
    {
        var x = transform.position.x + speed * Time.deltaTime;
        transform.position = new Vector3(x,transform.position.y,0);
    }
    
}
