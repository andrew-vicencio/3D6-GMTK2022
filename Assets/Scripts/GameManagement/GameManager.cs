using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _run;
    public bool running { get { return _run; }}

    private void Awake() {
        _run = true;

        GameObject game = GameObject.Find("Game");
        if (game != null) {
            game.SetActive(false);
        }
    }

    public void pause() {
        _run = false;
    }

    public void unpause() {
        _run = true;
    }
    
    public void OnDeath() {
        Debug.Log("You lose");
    }
}
