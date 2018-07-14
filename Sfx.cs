using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sfx : MonoBehaviour {

	public static Sfx instance;
	public AudioSource backgroundMusic;
	public AudioSource fallSound;
    public AudioSource pickSound;
    public AudioSource growSound;
    public AudioSource shrinkSound;
    public AudioSource deathSound;

	void Awake(){
        instance = this;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0){
            instance.gameObject.SetActive(false);
        }else{
            if(!instance.gameObject.activeSelf){
                instance.gameObject.SetActive(true);
            }
        }
        
    }

	public void PlayBackGroundMusic(){
		if (!backgroundMusic.isPlaying && !GameManager.instance.canPlay){
			backgroundMusic.Play();
		}
	}

	public void StopBackGroundMusic(){
		if (!GameManager.instance.canPlay)backgroundMusic.Stop();
	}

	public void PlayFallSound(){
		if (!GameManager.instance.canPlay)
            fallSound.PlayDelayed(1);
	}

	public void StopFallSound(){
		if (!GameManager.instance.canPlay)
            fallSound.Stop();
	}

    public void PlayGrowSound() {
        if (!GameManager.instance.canPlay)
            growSound.Play();
    }

    public void StopGrowSound() {
        if (!GameManager.instance.canPlay)
            growSound.Stop();
    }

    public void PlayShrinkSound() {
        if (!GameManager.instance.canPlay)shrinkSound.Play();
    }

    public void StopShrinkSound() {
        if (!GameManager.instance.canPlay)shrinkSound.Stop();
    }

    public void PlayPickSound() {
        if (!GameManager.instance.canPlay){
            float pitch = Random.Range(0.9f, 1.1f);
            pickSound.pitch = pitch;
            pickSound.Play();
        }
    }

    public void StopPickSound() {
        if (!GameManager.instance.canPlay)
            pickSound.Stop();
    }

    public void PlayDeathSound() {
        if (!GameManager.instance.canPlay)
            deathSound.Play();
    }

    public void StopDeathSound() {
        if (!GameManager.instance.canPlay)
            deathSound.Stop();
    }

	public void IncrementPitch(float val){
        if (!GameManager.instance.canPlay){
		    backgroundMusic.pitch += val;
            if (backgroundMusic.pitch >= GameManager.instance.maxPitchValue) {
                backgroundMusic.pitch = GameManager.instance.maxPitchValue;
            }
        }
	}

    public void SetPitch(float val) {
        if (!GameManager.instance.canPlay)
            backgroundMusic.pitch = val;
    }


}
