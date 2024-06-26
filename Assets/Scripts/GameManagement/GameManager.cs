using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _run;
    public bool running { get { return _run; }}
    public GameObject deathMenuUI;
    public MovementController mc;
    public ScoreScript sc;

    [SerializeField] private bool invuln;
    public bool invulnerable { get { return invuln; }}

    private void Awake() {
        _run = true;
        invuln = false;

        Time.timeScale = 1f;
        GameObject game = GameObject.Find("Game");
        if (game != null) {
            game.SetActive(false);
        }
    }

    public void pause() {
        _run = false;
        mc.enabled = false;
    }

    public void unpause() {
        _run = true;
        mc.enabled = true;
    }

    public void immortal(){
        invuln = true;
    }

    public void mortal(){
        invuln = false;
    }

    public void OnDeath() {
        //Time.timeScale = 0f;
        pause();
        deathMenuUI.SetActive(true);
    }

    public static void RestartGame()
    {
        Time.timeScale = 1f;
        Debug.Log("res");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void QuitGame()
    {
        Debug.Log("q");
        Application.Quit();
    }
}
