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

    public GameObject deathEffect;

    public Animator player;
    public Animator rawImage;

    private void Awake() {
        currentHealth = MAX_HEALTH;
        hp.texture = images[(int)(currentHealth - 1)];
        
        GameObject diceMan = GameObject.Find("DiceValueManager");
        changeDice = diceMan.GetComponent<ChangeDiceValue>();
    }

    public void damage(float damage = 1f) {
        currentHealth -= damage;
        hp.texture = images[(int)(currentHealth - 1)];
        player.SetTrigger("Hurt");
        rawImage.SetTrigger("Hurt");
        if (currentHealth > 0) {
            changeDice.newValue();
        } else {
            Die();
        }
    }

    public void heal(float heal = 1f) {
        currentHealth += heal;
    }

    public void Update() {
        if (currentHealth <= 0) {
            Die();
        }
    }

    public void Die(){
            m_Death.Invoke();
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);

    }
}
