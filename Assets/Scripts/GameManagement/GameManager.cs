using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _run;
    public bool running { get { return _run; }}
    public GameObject deathMenuUI;

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
        Time.timeScale = 0f;
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
