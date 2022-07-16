using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Vector3 direction = new Vector3(0,0,0);
    public GameObject bullet;
    public Transform bulletSpot;
    public ParticleSystem fire;
    public float coolDown = 0.5f;
    private float curCoolDown = 0;
    public CameraShake camShake;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1) && curCoolDown <= 0){
            fire.Play();
            Quaternion rot =   Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
            Instantiate(bullet,bulletSpot.position,rot);
            curCoolDown = coolDown;
            camShake.Shake(.1f,0.1f);
            
        }

        if(curCoolDown > 0){
            curCoolDown -= Time.deltaTime;
        }
        
    }
}
