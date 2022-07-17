using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;

	public float goalSize = 1.5f;

	Camera cam;
	
	void Awake()
	{
		originalPos = transform.localPosition;
		cam = GetComponent<Camera>();
	}
	
	public void Shake(float duration,float amount)
	{
		originalPos = transform.localPosition;
        shakeAmount = amount;
        shakeDuration = duration;
	}

	void Update()
	{

		if(cam.orthographicSize > goalSize+0.1f){
			cam.orthographicSize -= 0.1f;
		}
		else if(cam.orthographicSize < goalSize-0.1f){
			cam.orthographicSize += 0.1f;
		}

		if (shakeDuration > 0)
		{
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			transform.localPosition = originalPos;
		}
	}
}