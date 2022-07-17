using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityScript : MonoBehaviour
{
    private float curCoolDown = 0f;
    public ChangeDiceValue dv;
    MovementController mc;

    [Header("Charge")]
    public Animator charge;
    public ParticleSystem rage;
    public float chargeCoolDown = 5f;

    [Header("Dodge Roll")]
    public Animator playerAnim;
    public ParticleSystem dodge;
    public float dodgeCoolDown = 3f;

    [Header("Firewalk")]
    public GameObject explosive;
    public float teleportDist = 2f;
    public float teleCooldown = 6f;
    public ParticleSystem fire;

    [Header("Parry")]
    public Animator parry;
    public float parryCoolDown = 1f;

    [Header("Backstab")]
    public float maxDistance = 50f;
    public float distanceFromTarget = 1f;
    public float stabCoolDown = 5f;
    public ParticleSystem dust;

    [Header("Repelling Force")]
    public GameObject repelForce;
    public float repelForceCooldown = 5f;

    void Start(){
        mc = GetComponent<MovementController>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && curCoolDown <= 0){
            if(dv.currentValue == 1){
                Charge();
                
            }
            else if(dv.currentValue == 2){
                DodgeRoll();
            }
            else if(dv.currentValue == 3){
                FireWalk();
            }
            else if(dv.currentValue == 4){
                Parry();

            }
            else if(dv.currentValue == 5){
                BackStab();

            }
            else if(dv.currentValue == 6){
                RepellingForce();
            }
            
        }
        if(curCoolDown > 0){
            curCoolDown -= Time.deltaTime;
        }
        
    }

    public void Charge(){

    }

    public void DodgeRoll(){
        playerAnim.SetTrigger("Dodge");
        dodge.Play();
        curCoolDown = dodgeCoolDown;
    }

    public void FireWalk(){
        Quaternion rot =   Quaternion.Euler(0,transform.rotation.eulerAngles.y,-90);
        Vector3 movement = mc.direction.normalized;
        var obj = Instantiate(explosive,transform.position,rot);
        obj.GetComponent<ExplosiveBullet>().travelTime = 0;
        transform.position = transform.position + (movement *teleportDist);
        curCoolDown = teleCooldown;
        fire.Play();

    }

    public void Parry(){
        parry.SetTrigger("Parry");
    }

    public void BackStab(){
        dust.Play();
        //Debug.DrawRay(mc.transform.position,mc.direction,Color.white,10);
        Ray ray = new Ray(mc.transform.position, mc.direction);
        RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance);
        float distance = 0;
        GameObject victim = null;
        foreach(RaycastHit hit in hits){
            if(hit.collider.gameObject.tag == "Enemy"){
                float dist = Vector3.Distance(hit.collider.transform.position,transform.position);
                if(dist > distance){
                    distance = dist;
                    victim = hit.collider.gameObject;
                }
            }
        }
        if(victim != null){
            var distanceVec = victim.transform.position - transform.position;
            transform.position = victim.transform.position + (distanceVec.normalized * distanceFromTarget);
            curCoolDown = stabCoolDown;
        }
    }

    public void RepellingForce(){
        repelForce.SetActive(true);
        curCoolDown = repelForceCooldown;
    }
}
