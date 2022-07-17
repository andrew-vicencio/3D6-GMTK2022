using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*This script can be used on pretty much any gameObject. It provides several functions that can be called with 
animation events in the animation window.*/

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Animator setBoolInAnimator;

    // If we don't specify what audio source to play sounds through, just use the one on player.

    void Start()
    {
        if (!audioSource) audioSource = null ;
        
    }

    //Play a sound through the specified audioSource
    void PlaySound(AudioClip whichSound)
    {
        Debug.Log(audioSource);
        audioSource.PlayOneShot(whichSound);
        Debug.Log("Sound was played:" + whichSound);
    }


    public void EmitParticles(int amount)
    {
        particleSystem.Emit(amount);
    }

    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }

    public void SetAnimBoolToFalse(string boolName)
    {
        setBoolInAnimator.SetBool(boolName, false);
    }

    public void SetAnimBoolToTrue(string boolName)
    {
        setBoolInAnimator.SetBool(boolName, true);
    }

    public void LoadScene(string whichLevel)
    {
        SceneManager.LoadScene(whichLevel);
    }

    public void LoadSceneAdditive(string whichScene)
    {
        SceneManager.LoadScene(whichScene, LoadSceneMode.Additive);
        
    }

    public void UnloadScene(string whichScene)
    {
        SceneManager.UnloadSceneAsync(whichScene);

    }

    //Slow down or speed up the game's time scale!
    public void SetTimeScaleTo(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void KillMe()
    {
        Destroy(gameObject);
    }

    public void KillMyParent()
    {
        Destroy(transform.parent.gameObject);
    }
}
    