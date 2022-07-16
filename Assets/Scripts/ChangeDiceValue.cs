using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDiceValue : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;

    public Animator playerAnim;
    public Animator handCubeAnim;

    [HideInInspector] public int currentValue = 1;
    private List<int> basicNumbers = new List<int>();
    private List<int> randomNumbers;

    public float countdown = 30;
    private float curCountdown = 0;
    private bool once = true;
    // Start is called before the first frame update
    void Awake()
    {
        curCountdown = countdown;
        basicNumbers.Add(1);
        basicNumbers.Add(2);
        basicNumbers.Add(3);
        //basicNumbers.Add(4);
        basicNumbers.Add(5);
        basicNumbers.Add(6);
        randomNumbers = new List<int>(Shuffle(basicNumbers));
        currentValue = randomNumbers[0];
        randomNumbers.RemoveAt(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(curCountdown > 0){
            curCountdown -= Time.deltaTime;
        }
        else if(once){
            newValue();
            once = false;
        }
        
        if(player.gameObject.activeSelf){
            playerAnim.SetInteger("Value",currentValue);
        }
        if(hand.gameObject.activeSelf){
            handCubeAnim.SetInteger("Value",currentValue);
        }
        
        //on spacebar for testing purposes
        if(Input.GetKeyDown(KeyCode.Space)){
            newValue();
        }
        //if our list of random numbers is less than six append new list
        if(randomNumbers.Count <= 6){
            List<int> tempList = new List<int>(Shuffle(basicNumbers));
            randomNumbers.AddRange(tempList);
        }
    }

    public void newValue(){
        hand.SetActive(true);
        hand.transform.position = player.transform.position;
        

    }

    public void setValue(){
        currentValue = randomNumbers[0];
        randomNumbers.RemoveAt(0);
        curCountdown = countdown;
        once = true;
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
}
