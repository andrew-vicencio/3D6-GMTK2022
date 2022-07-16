using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] Camera cam;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;
    public float rotationSpeed = 5;

    void Start ()
    {
        body = GetComponent<Rigidbody>(); 
    }

    void Update ()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        var cursorPos = Input.mousePosition;
        var ray = cam.ScreenPointToRay(cursorPos);
        int layer_mask = LayerMask.GetMask("Ground");
        Vector3 targetPos = new Vector3(0,0,0);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
        {
            targetPos = new Vector3(hit.point.x,transform.position.y,hit.point.z);
        }
        Vector3 direction = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime *rotationSpeed);
        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
    }

    private void FixedUpdate()
    {  
        //move in direction of WASD
        Vector2 inputs = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        body.velocity = new Vector3(inputs.x,0,inputs.y).normalized;

       


    }

}
