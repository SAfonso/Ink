using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeScreen : MonoBehaviour {

	public static ShakeScreen instance;

	bool doingShake = false;
	float lengthTime;
	float timeFrequency;

    Vector3 oldPosition;

	[Range(0.00f, 0.5f)]
	public float intensity = 0.5f;
	[Range(0.001f, 0.1f)]
	public float rate = 0.5f;
	[Range(0.0f, 1.0f)]
	public float length = 0.5f;

	private void Awake()
	{
		if (instance != this) {
			DestroyImmediate(instance);  
		}
		instance = this;
        oldPosition = Camera.main.transform.position;
	}

	void Update () {
		if (doingShake) 
		{
			if (lengthTime < Time.time) 
			{
				doingShake = false;
                transform.position = oldPosition;

            }
			else
			{
				if (timeFrequency < Time.time) 
				{
					timeFrequency = Time.time + rate;
					transform.position = new Vector3(Random.Range (-intensity/2, intensity/2), Random.Range (-intensity, intensity),-1);
				}
			}
		}
	}
	public void DoScreenShake()
	{
		doingShake = true;

		lengthTime = Time.time + length;
		timeFrequency = Time.time + rate;
	}
}
