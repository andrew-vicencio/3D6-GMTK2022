using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject game;
    public GameObject player;
    public Vector3 cameraOffset;
    public float camSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if(game.activeSelf){
            Vector3 targetPos =  player.transform.position + cameraOffset;
            transform.position =Vector3.Lerp(transform.position, targetPos,camSpeed);
        }
        
    }
}
