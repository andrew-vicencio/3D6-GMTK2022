using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreScript : MonoBehaviour
{

    public float score = 0.0f;
    public int killScore = 0;
    public TMP_Text time;

    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.running){
            score += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(score);
            string str = span.ToString(@"hh\:mm\:ss\:ff");
            time.text = str;

        }

        
    }
}   
