using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float curCoolDown = 0;
    public CameraShake camShake;
    public ChangeDiceValue dv;

    [Header("Axe Attack")]
    public GameObject axe;
    public float axeCoolDown = 0.3f;
    public Animator axeAnim;


    [Header("Arrow Attack")]
    public GameObject bullet;
    public Transform bulletSpot;
    public ParticleSystem fire;
    public float arrowCoolDown = 0.5f;

    [Header("Explosive Attack")]
    public GameObject explosiveBullet;
    public float explosiveCoolDown = 1;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && curCoolDown <= 0){
            if(dv.currentValue == 1){
                SwingAxe();
                
            }
            else if(dv.currentValue == 2){
                FireArrow();
            }
            else if(dv.currentValue == 3){
                FireExplosives();
            }
            else if(dv.currentValue == 4){

            }
            else if(dv.currentValue == 5){

            }
            else if(dv.currentValue == 6){

            }
            
        }

        if(curCoolDown > 0){
            curCoolDown -= Time.deltaTime;
        }
        
    }

    void SwingAxe(){
        axeAnim.SetTrigger("Swing");
        curCoolDown = axeCoolDown;
    }

    void FireArrow(){
        fire.Play();
        Quaternion rot =   Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
        Instantiate(bullet,bulletSpot.position,rot);
        curCoolDown = arrowCoolDown;
        camShake.Shake(.1f,0.1f);

    }

    void FireExplosives(){
        fire.Play();
        Quaternion rot =   Quaternion.Euler(0,transform.rotation.eulerAngles.y,-90);
        Instantiate(explosiveBullet,bulletSpot.position,rot);
        curCoolDown = explosiveCoolDown;
        camShake.Shake(.1f,0.1f);

    }
}
