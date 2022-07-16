using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject game;
    public GameObject player;
    public Vector3 cameraOffset;

    // Update is called once per frame
    void Update()
    {
        if(game.activeSelf){
            transform.position = player.transform.position + cameraOffset;
        }
        
    }
}
