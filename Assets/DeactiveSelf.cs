using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveSelf : MonoBehaviour
{
    public void deactivate(){
        gameObject.SetActive(false);
    }
}
