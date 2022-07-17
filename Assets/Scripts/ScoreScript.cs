using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreScript : MonoBehaviour
{

    public float score = 0.0f;
    public int killScore = 0;
    //public TMP_Text time;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI timeUI;
    [SerializeField] private TextMeshProUGUI kill;

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.running){
            score += Time.deltaTime;
        }
        TimeSpan span = TimeSpan.FromSeconds(score);
        string str = span.ToString(@"hh\:mm\:ss\:ff");
        time.text = str;
        timeUI.text = str;
        kill.text = killScore.ToString();
        
    }
}   
