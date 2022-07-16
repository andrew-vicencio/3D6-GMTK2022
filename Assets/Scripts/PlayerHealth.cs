using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UnityEvent m_Death = new UnityEvent();
    private float MAX_HEALTH = 3;
    
    [SerializeField] private float currentHealth;

    private void Awake() {
        currentHealth = MAX_HEALTH;
    }

    public void damage(float damage = 1f) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            m_Death.Invoke();
        }
    }

    public void heal(float heal = 1f) {
        currentHealth += heal;
    }

    public void Update() {
        if (currentHealth <= 0) {
            m_Death.Invoke();
        }
    }
}
