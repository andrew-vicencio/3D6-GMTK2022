using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDiceValue : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            hand.SetActive(true);
            hand.transform.position = player.transform.position;
        }
    }
}
