using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Combat Stats")]
    public float maxHealth = 4;
    public float currentHealth;


    [Header("Movement")]
    public float moveSpeed = 7; //Max move speed
    public float sprintSpeed = 15;
    private float speed;

    [Header("Ground Checker")]
    public Transform groundChecker;
    public float checkRadius;
    public LayerMask groundLayer;
    private bool resetGroundAbilities = true;

  
    [Header("Jump")]
    public float jumpPower = 3;
    private int jumpsLeft = 0;
    private float fallForgivenessCounter;
    [SerializeField] private float fallForgiveness = .2f;
    private bool jumping;

    [Header("Jump Arc")]
    public float fallMultiplier = 2.5f;
    public float arcThreshold = 0.1f;

    [Header("Double Jump")]
    public bool AirJump = true;
    public float doubleJumpPower = 4;
    private bool isGrounded = false;
    public int numAirJumps = 3;


    
    [Header("Air Dash")]
    public bool AirDash = true;
    private bool dashing = false;
    private float dashTimer;
    public float dashForce = 20;
    private bool dashOff = false;
    public float dashCooldown = 0.15f;

    [Header("Ground Pound")]
    public bool AirSlam = true;
    private bool groundPound = false;
    public float airSlamPower = 8.0f;
    private float bouncePower;

    public GameObject poundEffect;


    private bool bounceback = false;

    [Header("Wall Jump")]
    public bool WallJump = true;
    public float jumpForgiveness = 0.2f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.3f;
    private bool isWallSliding = false;
    private RaycastHit2D WallCheckHit;
    private float lastDirection;
    private float wallSlideIncreasin;
    private float flipFlop = 0;
    float jumpTime;


    [Header("Other")]
    public Vector3 cameraOffset = new Vector3 (0.0f,0f,8.0f);
    private Rigidbody2D rigidBody;
    public Animator animator;
    public Animator spinAnimator;
    private float direction = 1;
    //public Runtime//animatorController defaultController;
    public ParticleSystem dust;
    public TrailRenderer trail;
    public ParticleSystem dash;
    private GameObject transformingInto;
    [HideInInspector] public bool isGhost = true;
    public int charReference;

    [Header("Dice Options")]
    public bool onLand = true;
    public Texture[] cubes;
    public RawImage cubeImg;
    public RawImage secondCubeImg;
    public RawImage bankCube;
    public RuntimeAnimatorController onPressController;
    public RuntimeAnimatorController onLandController;
    private int NextFace;
    private int CurrentFace = 2;
    private string abilityName;
    public TMP_Text test;
    private int bank = 0;
    private bool once = false;
    private List<int> basicNumbers = new List<int>();
    private List<int> randomNumbers;

    [Header("Bullet Options")]
    public GameObject bullet;
    public Transform bulletSpot;

    void Start()
    {
        basicNumbers.Add(1);
        basicNumbers.Add(2);
        basicNumbers.Add(3);
        basicNumbers.Add(4);
        basicNumbers.Add(5);
        basicNumbers.Add(6);
        currentHealth = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
        NextFace = UnityEngine.Random.Range(1, 7);
        cubeImg.texture = cubes[NextFace-1];
        randomNumbers = new List<int>(Shuffle(basicNumbers));
        cubeImg.texture = cubes[randomNumbers[0]-1];
        secondCubeImg.texture = cubes[randomNumbers[1]-1];
         

    }

    public List<int> Shuffle (List<int> shuffled){
        var rng = new System.Random();  
        int n = shuffled.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            var value = shuffled[k];  
            shuffled[k] = shuffled[n];  
            shuffled[n] = value;  
        }
        return shuffled; 
    }

    void Update()
    {
        if(onLand){
            spinAnimator.runtimeAnimatorController = onLandController;
        }
        else{
            spinAnimator.runtimeAnimatorController = onPressController;
        }

        Sprinting();
        Walk();
        Jump();
        CheckIfGrounded();
        //Float();
        BankDie();
        CameraFollowMe();
        JumpOffWalls();
        UseAbility();
        if(randomNumbers.Count <= 6){
            List<int> tempList = new List<int>(Shuffle(basicNumbers));
            randomNumbers.AddRange(tempList);
        }


    }

    private void BankDie(){
        if(bank == 0){
            bankCube.color = Color.clear;
        }
        if(onLand){

        }
        else{
            if(Input.GetKeyDown(KeyCode.B)){
                if(bank == 0){
                    bank = randomNumbers[0];
                    bankCube.texture = cubes[bank-1];
                    bankCube.color = Color.white;
                    randomNumbers.RemoveAt(0);
                    cubeImg.texture = cubes[randomNumbers[0]-1];
                    secondCubeImg.texture = cubes[randomNumbers[1]-1];
                }
                else{
                    var bankNum = bank;
                    bank = randomNumbers[0];
                    bankCube.texture = cubes[bank-1];
                    bankCube.color = Color.white;
                    randomNumbers[0] = bankNum;
                    cubeImg.texture = cubes[randomNumbers[0]-1];
                }
            
        }

        }
        


    }

    private void UseAbility(){

            if(CurrentFace == 1){
                abilityName = "Double Jump";
                AirJump = true;
            }
            else if(CurrentFace == 2){
                abilityName = "Air Dash";
                AirJump = false;
                Dash();
            }
            else if(CurrentFace == 3){
                abilityName = "Shoot";
                AirJump = false;
                Shoot();
            }
            else if(CurrentFace == 4){
                abilityName = "Double Jump";
                AirJump = true;
            }
            else if(CurrentFace == 5){
                abilityName = "Air Dash";
                AirJump = false;
                Dash();
            }
            else if(CurrentFace == 6){
                abilityName = "Shoot";
                AirJump = false;
                Shoot();
            }
        
        test.text = abilityName;
    }

    private void GroundPound(){
        
        if(groundPound){
            //animator.SetBool("pound",true);
        }
        else{
            //animator.SetBool("pound",false);
        }

        if(Input.GetKeyDown(KeyCode.Return) && !isGrounded && !groundPound && AirSlam && !bounceback){

            groundPound = true;
            RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up,Mathf.Infinity, groundLayer);
            if (hit.collider != null) {

                var bigger = Mathf.Max(3-(hit.distance/11),0);
                bouncePower = hit.distance*bigger; 

                Debug.Log("Ground Pound Power: "+ bouncePower);

            }
            else{
                Debug.Log("I can't find the ground!");
            }

    
        }
    }

    private void CameraFollowMe()
    {
        GameObject.Find("Main Camera").transform.position = transform.position + cameraOffset;
    }


    private void Sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
            ////animator.SetBool("Running",true);
            //trail.emitting = true;
        }
        else
        {
            speed = moveSpeed;
            ////animator.SetBool("Running",false);
            //trail.emitting = false;
        }

    }

    private void Dash()
    {
        
        if (dashing)
        {
            //trail.emitting = true;
            dashTimer -= Time.deltaTime;
            float x = Input.GetAxisRaw("Horizontal");
            float moveBy = dashForce * moveSpeed *direction;
            rigidBody.velocity = new Vector2(moveBy, rigidBody.velocity.y);
            if (dashTimer <= 0)
            {
                dashing = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && !dashOff && !isGrounded && AirDash)
        {
            dash.Play();
            ////animator.SetTrigger("dash");
            dashTimer = dashCooldown;
            dashing = true;
            dashOff = true;
        }
        else{
            //trail.emitting = false;
        }


    }

    private void Walk()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        if(!groundPound){
            if (x > 0.0f)
            {
                direction = 1;
                animator.SetBool("Moving",true);
            }
            else if (x < 0.0f)
            {
                direction = -1;
                animator.SetBool("Moving",true);
            }
            else{
                animator.SetBool("Moving",false);
            }

                rigidBody.velocity = new Vector2(moveBy, rigidBody.velocity.y);
        }
        else{
            animator.SetBool("Moving",false);
            rigidBody.velocity = new Vector2(0.0f, rigidBody.velocity.y);
        }
        
    }

    private void Shoot(){
        if(Input.GetKeyDown(KeyCode.Return)){
            Quaternion rot = transform.rotation;
            rot.eulerAngles =  new Vector3(0,0,-90);
            Instantiate(bullet,bulletSpot.position,rot);
        }

    }

    private void Jump(){

        if(rigidBody.velocity.y < arcThreshold && rigidBody.velocity.y != 0){
            rigidBody.velocity = rigidBody.velocity - new Vector2 (0,fallMultiplier);
        }
        if (rigidBody.velocity.y < -0.3)
        {
            //animator.SetBool("jump",false);
            //animator.SetBool("fall",true);
            if(onLand){
                spinAnimator.SetBool("airborne",true);
            }
            
            
            
        }
        else if (rigidBody.velocity.y > 0.3)
        {
            animator.SetBool("jump",true);
            if(onLand){
                spinAnimator.SetBool("airborne",true);
            }
            //animator.SetBool("fall",false);
        }
        else{
            animator.SetBool("jump",false);
            if(onLand){
                spinAnimator.SetBool("airborne",false);
                if(once){
                    CurrentFace = spinAnimator.GetInteger("rotation");
                }
                
            }
            //animator.SetBool("fall",false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || isWallSliding && Input.GetKeyDown(KeyCode.Space))
        {
            if(isWallSliding){
                flipFlop = lastDirection;
            }
            Bounce(jumpPower);
            if(onLand){
                spinAnimator.SetInteger("rotation",randomNumbers[0]);
                once = true;

            }
            else{
                spinAnimator.SetTrigger(NextFace.ToString());
                CurrentFace = randomNumbers[0];
                spinAnimator.SetTrigger(randomNumbers[0].ToString());
                randomNumbers.RemoveAt(0);
                cubeImg.texture = cubes[randomNumbers[0]-1];
                secondCubeImg.texture = cubes[randomNumbers[1]-1];

            }
            

        }
        else if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0 && AirJump)
        {
            //animator.SetTrigger("doubleJump");
            Bounce(doubleJumpPower);
            jumpsLeft = jumpsLeft - 1;
            if(onLand){

            }
            else{
                CurrentFace = randomNumbers[0];
                spinAnimator.SetTrigger(randomNumbers[0].ToString());
                randomNumbers.RemoveAt(0);
                cubeImg.texture = cubes[randomNumbers[0]-1];
                secondCubeImg.texture = cubes[randomNumbers[1]-1];

            }
            
        }
    }

    void Bounce(float power)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, power);
        jumping = true;
        dust.Play();
    }

    void JumpOffWalls(){

            var directionalDist = 0f;
            if(direction == 1){
                directionalDist = wallDistance;
                
            }
            else if(direction == -1){
                 directionalDist = wallDistance *-1;
            }

            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(directionalDist, 0), wallDistance, groundLayer);
            Debug.DrawRay(transform.position, new Vector2(directionalDist,0), Color.blue);

            if(WallCheckHit && !isGrounded && rigidBody.velocity.x != 0 && flipFlop != direction && WallJump && !groundPound){
                isWallSliding = true;
                jumpTime = Time.time + jumpForgiveness;
                lastDirection = direction;
                wallSlideIncreasin -= 0.02f;
                //animator.SetBool("walljump", true);
            }else if(jumpTime < Time.time){   
                isWallSliding = false;
                wallSlideIncreasin = -wallSlideSpeed;
            }else{
                //animator.SetBool("walljump", false);
            }



            if(isWallSliding){
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, wallSlideIncreasin, float.MaxValue));
            }
             
    }

    void NewNumber(){

    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundChecker.position, checkRadius, groundLayer);
        if (collider != null)
        {
            if (!isGrounded)
            {
                if(groundPound && !bounceback){
                    
                    Bounce(bouncePower);
                    bounceback = true;
                    groundPound = false;
                    resetGroundAbilities = false;
                }
                else if(resetGroundAbilities){
                    bounceback = false;
                    isGrounded = true;
                    jumping = false;
                    dashOff = false;
                    fallForgivenessCounter = 0;
                    jumpsLeft = numAirJumps;
                    flipFlop = 0;
                }
 
            }
            

        }
        else
        {
            if (fallForgivenessCounter < fallForgiveness && !jumping)
            {
                fallForgivenessCounter += Time.deltaTime;
            }
            else
            {
                isGrounded = false;
                resetGroundAbilities = true;
            }
        }
    }

}

