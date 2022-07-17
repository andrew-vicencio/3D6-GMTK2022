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

    private void Awake() {
        currentHealth = MAX_HEALTH;
    }

    private void Start() {
        Transform canvas = transform.Find("UI");
        healthbar = canvas.Find("Healthbar").GetComponent<Slider>();
        healthbar.maxValue = MAX_HEALTH;
        healthbar.value = MAX_HEALTH;
    }

    public void damage(float damage = 25) {
        currentHealth -= damage;
        healthbar.value = currentHealth;

        if (currentHealth <= 0) {
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
