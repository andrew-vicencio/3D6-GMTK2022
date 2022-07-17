using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private GameManager gameManager;

    Rigidbody body;
    [SerializeField] Camera cam;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;
    public float rotationSpeed = 5;

    public float dodgeSpeed = 10.0f;

    public float chargeSpeed = 10.0f;

    public Animator anim;
    public Vector3 direction = new Vector3(0,0,0);

    public bool movementLocked = false;

    public bool charging = false;

    Vector3 staticDir = new Vector3(0,0,0);

    public Vector3 targetPos = new Vector3(0,0,0);

    public ChangeDiceValue dv;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start ()
    {
        body = GetComponent<Rigidbody>(); 
    }

    void Update ()
    {
        if (!gameManager.running) {
            return;
        }

        if(dv.currentValue != 2 && movementLocked){
            movementLocked = false;

        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        var cursorPos = Input.mousePosition;
        var ray = cam.ScreenPointToRay(cursorPos);
        int layer_mask = LayerMask.GetMask("Ground");
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
        {
            targetPos = new Vector3(hit.point.x,transform.position.y,hit.point.z);
        }
        direction = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime *rotationSpeed);
        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
        
        if(body.velocity.x > 0.1 || body.velocity.z > 0.1 || body.velocity.x < -0.1 || body.velocity.z < -0.1){
            anim.SetBool("Run",true);
        }
        else{
            anim.SetBool("Run",false);
        }
    }

    private void FixedUpdate()
    {  
        //move in direction of WASD
        if(!movementLocked && !charging){
            Vector2 inputs = new Vector2(horizontal, vertical).normalized * runSpeed;
            body.velocity = new Vector3(inputs.x,0,inputs.y);
        }
        else if(movementLocked){
            Vector2 force = new Vector2(staticDir.x,staticDir.z).normalized * dodgeSpeed;
            body.velocity = new Vector3(force.x,0,force.y);
        }
        else{
            Vector2 force = new Vector2(direction.x,direction.z).normalized * chargeSpeed;
            body.velocity = new Vector3(force.x,0,force.y);

        }
    }

    public void lockMovement(){
        movementLocked = true;
        staticDir = direction;
    }

    public void movementUnlocked(){
        movementLocked = false;
    }

}
