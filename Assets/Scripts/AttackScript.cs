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

    [Header("Knife Fan Attack")]
    public GameObject knifeBullet;
    public float knifeCoolDoown = 1;

    [Header("Magic Missile Attack")]
    public GameObject magicMissile;
    public float magicCoolDown = 1;
    private float burstCoolDown = 0.05f;
    private int misslesLaunched = 0;
    
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
                FireKnives();

            }
            else if(dv.currentValue == 6){
                MagicMissle();
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

    void FireKnives(){
        fire.Play();
        Quaternion rot =   Quaternion.Euler(0,transform.rotation.eulerAngles.y,-90);
        Instantiate(knifeBullet,bulletSpot.position,rot);
        curCoolDown = knifeCoolDoown;
        camShake.Shake(.1f,0.1f);

    }

    void MagicMissle(){
        misslesLaunched += 1;
        fire.Play();
        float Xrot = Random.Range(-15f,15f);
        Quaternion rot =   Quaternion.Euler(Xrot,transform.rotation.eulerAngles.y,0);
        Instantiate(magicMissile,bulletSpot.position,rot);
        camShake.Shake(.05f,0.1f);
        if(misslesLaunched >= 6){
            curCoolDown = magicCoolDown;
            misslesLaunched = 0;
        }
        else{
            curCoolDown = burstCoolDown;
        }
        
        

    }
}
