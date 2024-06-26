using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public bool mainMenu = true;
    Animator anim;
    public GameObject menuCanvas;
    public GameObject game;

    public Animator hand;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Play(){
        anim.SetTrigger("MoveGame");
        menuCanvas.SetActive(false);
        hand.enabled = false;

    }

    public void setGameActive(){
        game.SetActive(true);
    }
}
