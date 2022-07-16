using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HidePlayer : MonoBehaviour
{
    public GameObject player;
    public CameraShake camShake;
    public ParticleSystem cloud;
    public UnityEvent changeDieValue;

    public void hidePlayer(){
        player.SetActive(false);
    }

    public void showPlayer(){
        player.SetActive(true);
    }

    public void hideSelf(){
        transform.parent.gameObject.SetActive(false);
    }

    public void quickShake(float magnitude){
        camShake.Shake(.1f,magnitude);
    }

    public void playParticleEffect(){
        cloud.Play();
    }

    public void changeValue(){
        changeDieValue.Invoke();
    }

}
