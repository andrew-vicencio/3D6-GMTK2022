using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "My-project/GameState", order = 0)]
public class GameState : ScriptableObject {
    public bool run;

    // PLAYER DATA
    public float p_MAX_HEALTH;
    public float p_MOVEMENT_SPEED;

    public float p_DAMAGE_1;
    public float p_DAMAGE_2;
    public float p_DAMAGE_3;
    public float p_DAMAGE_4;
    public float p_DAMAGE_5;
    public float p_DAMAGE_6;

    public float e_MAX_HEALTH;
    public float e_MAX_MOVEMENT_SPEED;
    public float e_MELEE_DAMAGE;
    public float e_RANGE_DAMAGE;
}
