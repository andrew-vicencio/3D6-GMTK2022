using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathMenuUI;

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
