using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private float MAX_HEALTH = 100;
    [SerializeField] private float currentHealth;
    private Slider healthbar;

    private void Awake() {
        currentHealth = MAX_HEALTH;
    }

    private void Start() {
        Transform canvas = transform.Find("Canvas");
        healthbar = canvas.Find("Slider").GetComponent<Slider>();
        healthbar.maxValue = MAX_HEALTH;
        healthbar.value = MAX_HEALTH;
    }

    public void damage(float damage = 25) {
        currentHealth -= damage;
        healthbar.value = currentHealth;

        if (currentHealth <= 0) {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }

    public void heal(float heal = 1f) {
        currentHealth += heal;
        healthbar.value = currentHealth;
    }
}
