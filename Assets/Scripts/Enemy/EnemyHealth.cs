using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float MAX_HEALTH = 100;
    private float currentHealth;
    private Slider healthbar;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;

    private ScoreScript score;

    void PlaySound(AudioClip whichSound)
    {
        Debug.Log(audioSource);
        audioSource.PlayOneShot(whichSound);
        Debug.Log("Sound was played:" + whichSound);
    }

    private void Awake() {
        currentHealth = MAX_HEALTH;
        score = GameObject.Find("GameManager").GetComponent<GameManager>().sc;
    }

    private void Start() {
        Transform canvas = transform.Find("UI");
        healthbar = canvas.Find("Healthbar").GetComponent<Slider>();
        healthbar.maxValue = MAX_HEALTH;
        healthbar.value = MAX_HEALTH;
    }

    public void damage(float damage = 25) {
        //PlaySound(hurtSound);
        currentHealth -= damage;
        healthbar.value = currentHealth;

        if (currentHealth <= 0) {
            score.killScore ++;
            Debug.Log("Dead");
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void heal(float heal = 1f) {
        currentHealth += heal;
        healthbar.value = currentHealth;
    }
}
