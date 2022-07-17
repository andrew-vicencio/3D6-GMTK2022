using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeScript : MonoBehaviour
{
    
    public MovementController mc;

    public void movementLocked(){
        mc.lockMovement();
    }

    public void movementUnlocked(){
        mc.movementUnlocked();
    }


}
