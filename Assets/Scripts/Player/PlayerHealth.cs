using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UnityEvent m_Death = new UnityEvent();

    private float MAX_HEALTH = 3;
    [SerializeField] private float currentHealth;

    public RawImage hp;
    public Texture[] images;

    private void Awake() {
        currentHealth = MAX_HEALTH;
        hp.texture = images[(int)(currentHealth - 1)];
    }

    public void damage(float damage = 1f) {
        currentHealth -= damage;
        hp.texture = images[(int)(currentHealth - 1)];

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