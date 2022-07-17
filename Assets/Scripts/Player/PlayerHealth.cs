using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UnityEvent m_Death = new UnityEvent();

    private float MAX_HEALTH = 3;
    [SerializeField] public static float currentHealth;

    public RawImage hp;
    public Texture[] images;

    private ChangeDiceValue changeDice;

    private void Awake() {
        currentHealth = MAX_HEALTH;
        hp.texture = images[(int)(currentHealth - 1)];
        
        GameObject diceMan = GameObject.Find("DiceValueManager");
        changeDice = diceMan.GetComponent<ChangeDiceValue>();
    }

    public void damage(float damage = 1f) {
        currentHealth -= damage;
        hp.texture = images[(int)(currentHealth - 1)];

        if (currentHealth > 0) {
            changeDice.newValue();
        } else {
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
